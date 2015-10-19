using System;
using System.Collections.Generic;
using System.Security.Policy;
using FitnessDataAnalyzer.Data;

namespace FitnessDataAnalyzer.ViewModel
{
   public class ProgressViewModel : IProgressViewModel
   {
      public ProgressViewModel()
      {
         DataPoints = new Dictionary<DateTime, IDataPoint>();
      }

      public void AddDataPoint(IDataPoint point)
      {
         DataPoints.Add(point.Date, point);
      }

      public IDictionary<DateTime, IDataPoint> DataPoints { get; }
   }
}
