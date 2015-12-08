using System;
using System.Collections.Generic;
using System.Reactive;
using System.Windows.Forms;
using FitnessDataAnalyzer.Data.Interfaces;
using FitnessDataAnalyzer.ViewModel;

namespace FitnessDataAnalyzer.View
{
   public interface IProgressView
   {
      IProgressViewModel ViewModel { get; set; }

      void SetStatusStripText(string text);

      void PlotDataPoints(IDictionary<DateTime, IDataPoint> highActivityDataPoints,
                          IDictionary<DateTime, IDataPoint> lowActivityDataPoints);

      void BuildTree(IEnumerable<TreeNode> nodes);

      IObservable<string> GetLoadWatchDataClicks();

      IObservable<string> GetLoadFitnotesDataClicks();

      IObservable<Unit> GetClearLoadedDataClicks();

      IObservable<TreeNode> GetTreeNodeSelectionChanges();

      void Clear();
   }
}
