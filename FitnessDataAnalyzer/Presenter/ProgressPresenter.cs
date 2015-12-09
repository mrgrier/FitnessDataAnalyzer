using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive.Disposables;
using System.Windows.Forms;
using FitnessDataAnalyzer.Data;
using FitnessDataAnalyzer.Data.Interfaces;
using FitnessDataAnalyzer.View;
using FitnessDataAnalyzer.ViewModel;

namespace FitnessDataAnalyzer.Presenter
{
   public class ProgressPresenter : IProgressPresenter
   {
      public ProgressPresenter(IProgressView view)
      {
         m_view = view;
         m_viewModel = m_view.ViewModel = new ProgressViewModel();

         m_lowHRThreshold = 70;
         m_highHRThreshold = 110;

         Subscribe();
      }

      private void Subscribe()
      {
         if(m_subscriptions != null && !m_subscriptions.IsDisposed)
         {
            m_subscriptions.Dispose();
         }

         m_subscriptions = new CompositeDisposable
         {
            m_view.GetLoadWatchDataClicks().Subscribe(LoadWatchData),
            m_view.GetLoadFitnotesDataClicks().Subscribe(LoadExerciseData),
            m_view.GetClearLoadedDataClicks().Subscribe(_ => HandleClearClicks()),
            m_view.GetTreeNodeSelectionChanges().Subscribe(HandleNodeSelection)
         };
      }

      private void LoadWatchData(string filePath)
      {
         var stopwatch = Stopwatch.StartNew();
         var numberOfPoints = 0;

         m_viewModel.DataPoints.Clear();

         try
         {
            m_view.SetStatusStripText(LOADING);

            using(var reader = new StreamReader(File.OpenRead(filePath)))
            {
               if(!reader.EndOfStream)
                  reader.ReadLine(); // skip the first line.

               while(!reader.EndOfStream)
               {
                  var line = reader.ReadLine();
                  if(string.IsNullOrEmpty(line))
                     continue;

                  var point = CreateDataPoint(line.Split(','));

                  m_viewModel.DataPoints[point.Date] = point;
                  AnalyzeDataPoint(point);

                  numberOfPoints++;
               }
            }

            stopwatch.Stop();
            m_view.SetStatusStripText($"{numberOfPoints} watch data points loaded in " +
                                      $"{stopwatch.ElapsedMilliseconds} milliseconds");

            m_viewModel.WatchDataNotYetLoaded = false;
         }
         catch(Exception e)
         {
            stopwatch.Stop();
            m_viewModel.DataPoints.Clear();
            m_view.SetStatusStripText(FILE_READ_ERROR);
            MessageBox.Show(e.Message, FILE_READ_ERROR);
         }

         m_view.PlotDataPoints(m_viewModel.HighActivityDataPoints,
                               m_viewModel.LowActivityDataPoints);
      }

      private static IDataPoint CreateDataPoint(IReadOnlyList<string> values)
      {
         DateTime date;
         DateTime.TryParse(values[0], out date);

         double calories;
         double.TryParse(values[1], out calories);

         double perspiration;
         double.TryParse(values[2], out perspiration);

         double heartRate;
         double.TryParse(values[3], out heartRate);

         double skinTemp;
         double.TryParse(values[4], out skinTemp);

         int steps;
         int.TryParse(values[5], out steps);

         return new DataPoint(date, calories, perspiration, heartRate, skinTemp, steps);
      }

      private void AnalyzeDataPoint(IDataPoint point)
      {
         // if the user wasn't wearing it during this point's minute.
         if(point.HeartRate <= 0)
            return;

         if(point.HeartRate < m_lowHRThreshold)
            m_viewModel.LowActivityDataPoints[point.Date] = point;
         else if(point.HeartRate > m_highHRThreshold)
            m_viewModel.HighActivityDataPoints[point.Date] = point;
      }

      private void LoadExerciseData(string filePath)
      {
         var stopwatch = Stopwatch.StartNew();
         var numberOfPoints = 0;

         var treeNodes = new List<TreeNode>();

         try
         {
            m_view.SetStatusStripText(LOADING);

            using(var reader = new StreamReader(File.OpenRead(filePath)))
            {
               if(!reader.EndOfStream)
                  reader.ReadLine(); // skip the first line.

               while(!reader.EndOfStream)
               {
                  var line = reader.ReadLine();
                  if(line == null)
                     continue;

                  LoadSet(line.Split(','), treeNodes);

                  numberOfPoints++;
               }
            }

            stopwatch.Stop();
            m_view.SetStatusStripText($"{numberOfPoints} FitNotes data points loaded in " +
                                      $"{stopwatch.ElapsedMilliseconds} milliseconds");

            m_viewModel.SetDataNotYetLoaded = false;
         }
         catch(Exception e)
         {
            stopwatch.Stop();

            m_viewModel.Categories.Clear();
            treeNodes.Clear();

            m_view.SetStatusStripText(FILE_READ_ERROR);
            MessageBox.Show(e.Message, FILE_READ_ERROR);
         }

         m_view.BuildTree(treeNodes);
      }

      private void LoadSet(IReadOnlyList<string> values, List<TreeNode> treeNodes)
      {
         ICategory category;
         if(!m_viewModel.Categories.TryGetValue(values[2], out category))
         {
            category = new Category(values[2]);
            m_viewModel.Categories[category.Name] = category;
            treeNodes.Add(new TreeNode(category.Name) { Tag = category });
         }

         IExercise exercise;
         if(!category.Exercises.TryGetValue(values[1], out exercise))
         {
            exercise = new Exercise(values[1], category);
            category.Exercises[exercise.Name] = exercise;

            var parentNode = treeNodes.Find(x => x.Text == category.Name);
            if(parentNode == null)
            {
               throw new Exception(
                  $"could not find category parent node for exercise: {exercise.Name}");
            }

            parentNode.Nodes.Add(new TreeNode(exercise.Name) { Tag = exercise });
         }

         var date = DateTime.Parse(values[0]);

         if(!string.IsNullOrEmpty(values[5]) &&
            !string.IsNullOrEmpty(values[6]) &&
            !string.IsNullOrEmpty(values[7]))
         {
            // this is a distance exercise
            var distance = double.Parse(values[5]);
            var distanceUnit = ParseDistanceUnit(values[6]);
            var duration = TimeSpan.Parse(values[7]);

            var set = new DistanceSet(date, distance, distanceUnit, duration);
            exercise.Sets[set.Date] = set;
         }
         else if(!string.IsNullOrEmpty(values[3]) && !string.IsNullOrEmpty(values[4]))
         {
            // this is a weighted exercise
            var weight = double.Parse(values[3]);
            var reps = int.Parse(values[4]);

            var set = new WeightedSet(date, weight, reps);
            exercise.Sets[set.Date] = set;
         }
         else
         {
            throw new Exception("Entry did not match format for distance nor weighted exercise.");
         }
      }

      private DistanceUnit ParseDistanceUnit(string unit)
      {
         if(unit == null)
            throw new ArgumentNullException(nameof(unit));

         if(unit.ToLower().Contains("mi"))
            return DistanceUnit.Miles;
         if(unit.ToLower().Contains("ki"))
            return DistanceUnit.Kilometers;

         throw new Exception("Distance unit not recognized");
      }

      private void HandleClearClicks()
      {
         m_viewModel.Clear();
         m_view.Clear();
         m_view.SetStatusStripText("Data cleared.");
      }

      private void HandleNodeSelection(TreeNode node)
      {
         var exercise = node.Tag as IExercise;

         if(exercise == null)
            return;

         m_view.PlotExercisePoints(exercise.Name, exercise.Sets);
      }

      private readonly IProgressView m_view;
      private readonly IProgressViewModel m_viewModel;
      private CompositeDisposable m_subscriptions;
      private readonly int m_lowHRThreshold;
      private readonly int m_highHRThreshold;

      private const string FILE_READ_ERROR = "Error reading file";
      private const string LOADING = "Loading...";
   }
}
