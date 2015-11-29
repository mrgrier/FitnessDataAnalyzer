using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;
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

      public IProgressViewModel ViewModel { get; set; }

      public void SetStatusStripText(string text)
      {
         toolStripStatusLabel1.Text = text;
         statusStrip1.Refresh();
      }

      public void RefreshDataPoints()
      {
         if(ViewModel == null)
            return;

         // plot high activity heart rate data.
         ExerciseChart.Series[0].Points.Clear();
         foreach(var point in ViewModel
                              .HighActivityDataPoints
                              .OrderBy(x => x.Key)
                              .Select(kv => kv.Value)
                              .Smooth(GetChunkSize(ViewModel.HighActivityDataPoints.Count)))
         {
            ExerciseChart.Series[0].Points.AddXY(point.Date.ToOADate(), point.HeartRate);
         }

         // plot low activity heart rate data.
         ExerciseChart.Series[1].Points.Clear();
         foreach(var point in ViewModel
                              .LowActivityDataPoints
                              .OrderBy(x => x.Key)
                              .Select(kv => kv.Value)
                              .Smooth(GetChunkSize(ViewModel.LowActivityDataPoints.Count)))
         {
            ExerciseChart.Series[1].Points.AddXY(point.Date.ToOADate(), point.HeartRate);
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

      private const string CSV_FILTER = "Comma Separated Values | *.csv";
      private const int DATA_POINT_MAX_COUNT = 20;
   }
}
