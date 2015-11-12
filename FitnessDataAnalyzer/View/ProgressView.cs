using System;
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

      public void RefreshDataPoints()
      {
         if(ViewModel == null)
            return;

         // plot high activity heart rate data.
         ExerciseChart.Series[0].Points.Clear();
         foreach(var point in ViewModel
                              .HighActivityDataPoints
                              .OrderBy(x => x.Key)
                              .SkipEvery(GetSkipFactor(ViewModel.HighActivityDataPoints.Count)))
         {
            ExerciseChart.Series[0].Points.AddXY(point.Key.ToOADate(), point.Value.HeartRate);
         }

         // plot low activity heart rate data.
         ExerciseChart.Series[1].Points.Clear();
         foreach(var point in ViewModel
                              .LowActivityDataPoints
                              .OrderBy(x => x.Key)
                              .SkipEvery(GetSkipFactor(ViewModel.LowActivityDataPoints.Count)))
         {
            ExerciseChart.Series[1].Points.AddXY(point.Key.ToOADate(), point.Value.HeartRate);
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

      private static int GetSkipFactor(int dataPointCount)
      {
         return dataPointCount / DATA_POINT_MAX_COUNT;
      }

      private readonly IProgressPresenter m_presenter;

      private const string CSV_FILTER = "Comma Separated Values | *.csv";
      private const int DATA_POINT_MAX_COUNT = 20;
   }
}
