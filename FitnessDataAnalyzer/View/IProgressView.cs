using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FitnessDataAnalyzer.ViewModel;

namespace FitnessDataAnalyzer.View
{
   public interface IProgressView
   {
      IProgressViewModel ViewModel { get; set; }
      void SetStatusStripText(string text);
      void RefreshDataPoints();
      void BuildTree(IEnumerable<TreeNode> nodes);
      IObservable<string> GetLoadWatchDataClicks();
      IObservable<string> GetLoadFitnotesDataClicks();
      IObservable<TreeNode> GetTreeNodeSelectionChanges();
   }
}
