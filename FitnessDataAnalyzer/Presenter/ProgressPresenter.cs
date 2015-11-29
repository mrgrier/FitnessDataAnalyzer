﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
         m_subscriptions = new CompositeDisposable
         {
            m_view.GetLoadWatchDataClicks().Subscribe(LoadDataPointsFromFile),
            m_view.GetLoadFitnotesDataClicks().Subscribe(LoadExerciseData)
         };
      }

      private void LoadDataPointsFromFile(string filePath)
      {
         var stopwatch = Stopwatch.StartNew();
         var numberOfPoints = 0;

         m_viewModel.DataPoints.Clear();

         try
         {
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
            m_view.SetStatusStripText($"{numberOfPoints} data points loaded in " +
                                      $"{stopwatch.ElapsedMilliseconds} milliseconds");
         }
         catch(Exception e)
         {
            stopwatch.Stop();
            m_viewModel.DataPoints.Clear();
            m_view.SetStatusStripText(FILE_READ_ERROR);
            MessageBox.Show(e.Message, FILE_READ_ERROR);
         }

         m_view.RefreshDataPoints();
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
            using(var reader = new StreamReader(File.OpenRead(filePath)))
            {
               if(!reader.EndOfStream)
                  reader.ReadLine(); // skip the first line.

               while(!reader.EndOfStream)
               {
                  var line = reader.ReadLine();
                  if(line == null)
                     continue;

                  var values = line.Split(',');

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

                  numberOfPoints++;
               }
            }

            stopwatch.Stop();
            m_view.SetStatusStripText($"{numberOfPoints} data points loaded in " +
                                      $"{stopwatch.ElapsedMilliseconds} milliseconds");
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

      //private IExercise CreateExercise(IReadOnlyList<string> values)
      //{
      //   // get or add category.
      //   var categoryName = values[2];
      //   var category = GetCategory(categoryName);

      //   // get or add exercise.
      //   var exerciseName = values[1];
      //   var exercise = GetExercise(category, exerciseName);

      //   DateTime date;
      //   DateTime.TryParse(values[0], out date);

      //   if(!string.IsNullOrEmpty(values[5]) &&
      //      !string.IsNullOrEmpty(values[6]) &&
      //      !string.IsNullOrEmpty(values[7]))
      //   {
      //      // this is a distance exercise
      //      var distance = double.Parse(values[5]);
      //      var distanceUnit = ParseDistanceUnit(values[6]);
      //      var duration = TimeSpan.Parse(values[7]);

      //      var distanceSet = new DistanceSet(exercise, date, distance, distanceUnit, duration);

      //      // TODO: add to view model.
      //   }

      //   else if(!string.IsNullOrEmpty(values[3]) && !string.IsNullOrEmpty(values[4]))
      //   {
      //      // this is a weighted exercise
      //      var weight = double.Parse(values[3]);
      //      var reps = int.Parse(values[4]);

      //      var weightedSet = new WeightedSet(exercise, date, weight, reps);

      //      // TODO: add to view model.
      //   }

      //   throw new Exception("Entry did not match format for distance nor weighted exercise.");
      //}

      private IExercise GetExercise(ICategory category, string exerciseName)
      {
         IExercise exercise;
         return category.Exercises.TryGetValue(exerciseName, out exercise) ?
                  exercise :
                  new Exercise(exerciseName, category);
      }

      private DistanceUnit ParseDistanceUnit(string unit)
      {
         if(unit == null)
            throw new ArgumentNullException(nameof(unit));

         if(unit.ToLower().Contains("mi"))
            return DistanceUnit.Miles;
         if(unit.ToLower().Contains("ki"))
            return DistanceUnit.Kilometers;

         // TODO: create a custom exception for this case.
         throw new Exception("Distance unit not recognized");
      }

      private readonly IProgressView m_view;
      private readonly IProgressViewModel m_viewModel;
      private CompositeDisposable m_subscriptions;
      private readonly int m_lowHRThreshold;
      private readonly int m_highHRThreshold;

      private const string FILE_READ_ERROR = "Error reading file";
   }
}
