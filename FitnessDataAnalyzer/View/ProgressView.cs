using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FitnessDataAnalyzer.Data.Interfaces;
using FitnessDataAnalyzer.Extensions;
using FitnessDataAnalyzer.Presenter;
using FitnessDataAnalyzer.ViewModel;

namespace FitnessDataAnalyzer.View
{
   public partial class ProgressView : Form, IProgressView
   {
      public ProgressView()
      {
         InitializeComponent();
         m_presenter = new ProgressPresenter(this);
      }

      public IProgressViewModel ViewModel
      {
         get { return m_viewModel; }
         set
         {
            m_viewModel = value;
            ViewModelBindingSource.DataSource = value;
         }
      }

      public void SetStatusStripText(string text)
      {
         toolStripStatusLabel1.Text = text;
         statusStrip1.Refresh();
      }

      public void Clear()
      {
         TreeView.Nodes.Clear();
         TreeView.Refresh();
         
         foreach(var series in ExerciseChart.Series)
         {
            series.Points.Clear();
         }

         ExerciseChart.Refresh();
      }

      public void PlotDataPoints(IDictionary<DateTime, IDataPoint> highActivityDataPoints,
                                 IDictionary<DateTime, IDataPoint> lowActivityDataPoints)
      {
         if(highActivityDataPoints == null || lowActivityDataPoints == null)
            return;

         // plot high activity heart rate data.
         ExerciseChart.Series[0].Points.Clear();
         foreach(var point in highActivityDataPoints
                              .OrderBy(x => x.Key)
                              .Select(kv => kv.Value)
                              .Smooth(GetChunkSize(ViewModel.HighActivityDataPoints.Count)))
         {
            ExerciseChart.Series[0].Points.AddXY(point.Date.ToOADate(), point.HeartRate);
         }

         // plot low activity heart rate data.
         ExerciseChart.Series[1].Points.Clear();
         foreach(var point in lowActivityDataPoints
                              .OrderBy(x => x.Key)
                              .Select(kv => kv.Value)
                              .Smooth(GetChunkSize(ViewModel.LowActivityDataPoints.Count)))
         {
            ExerciseChart.Series[1].Points.AddXY(point.Date.ToOADate(), point.HeartRate);
         }
      }

      public void PlotExercisePoints(string exerciseName, IDictionary<DateTime, ISet> sets)
      {
         ExerciseChart.Series[2].Points.Clear();

         ExerciseChart.Series[2].Name = exerciseName;

         foreach(var weightedSet in sets
                                    .OrderBy(x => x.Key)
                                    .Select(kv => kv.Value)
                                    .OfType<IWeightedSet>().Select(set => set))
         {
            ExerciseChart.Series[2].Points.AddXY(weightedSet.Date, weightedSet.Weight);
         }
      }

      public void BuildTree(IEnumerable<TreeNode> nodes)
      {
         TreeView.Nodes.Clear();

         foreach(var node in nodes.OrderBy(x => x.Text))
         {
            TreeView.Nodes.Add(node);
         }
      }

      public IObservable<string> GetLoadWatchDataClicks()
      {
         return Observable.FromEventPattern(
            evt => btnLoadWatchData.Click += evt,
            evt => btnLoadWatchData.Click -= evt)
            .Select(_ => GetFilePath())
            .Where(x => x != null);
      }

      public IObservable<string> GetLoadFitnotesDataClicks()
      {
         return Observable.FromEventPattern(
            evt => btnLoadFitnotesData.Click += evt,
            evt => btnLoadFitnotesData.Click -= evt)
            .Select(_ => GetFilePath())
            .Where(x => x != null);
      }

      public IObservable<Unit> GetClearLoadedDataClicks()
      {
         return Observable.FromEventPattern(
            evt => ClearButton.Click += evt,
            evt => ClearButton.Click -= evt)
            .Select(_ => Unit.Default);
      }

      public IObservable<TreeNode> GetTreeNodeSelectionChanges()
      {
         return Observable.FromEventPattern<TreeViewEventHandler, TreeViewEventArgs>(
            evt => TreeView.AfterSelect += evt,
            evt => TreeView.AfterSelect -= evt)
            .Select(e => e.EventArgs.Node)
            .Where(x => x != null);
      }

      private string GetFilePath()
      {
         using(var dialog = new OpenFileDialog())
         {
            dialog.Filter = CSV_FILTER;
            return dialog.ShowDialog(this) == DialogResult.OK ? dialog.FileName : null;
         }
      }

      private static int GetChunkSize(int dataPointCount)
      {
         return dataPointCount / DATA_POINT_MAX_COUNT;
      }

      private readonly IProgressPresenter m_presenter;
      private IProgressViewModel m_viewModel;

      private const string CSV_FILTER = "Comma Separated Values | *.csv";
      private const int DATA_POINT_MAX_COUNT = 30;
   }
}
