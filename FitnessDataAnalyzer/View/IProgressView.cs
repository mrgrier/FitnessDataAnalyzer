using System;
using FitnessDataAnalyzer.ViewModel;

namespace FitnessDataAnalyzer.View
{
   public interface IProgressView
   {
      IProgressViewModel ViewModel { get; set; }
      void SetStatusStripText(string text);
      void RefreshDataPoints();
      IObservable<string> GetLoadWatchDataClicks();
      IObservable<string> GetLoadFitnotesDataClicks();
   }
}
