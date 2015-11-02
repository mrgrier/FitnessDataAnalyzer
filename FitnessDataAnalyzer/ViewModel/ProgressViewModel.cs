using System;
using System.Collections.Generic;
using FitnessDataAnalyzer.Data;

namespace FitnessDataAnalyzer.ViewModel
{
   public class ProgressViewModel : IProgressViewModel
   {
      public ProgressViewModel()
      {
         DataPoints = new Dictionary<DateTime, IDataPoint>();
         Categories = new List<ICategory>();
      }

      public void AddDataPoint(IDataPoint point)
      {
         DataPoints[point.Date] = point;
      }

      public IDictionary<DateTime, IDataPoint> DataPoints { get; }

      public IList<ICategory> Categories { get; }
   }
}
