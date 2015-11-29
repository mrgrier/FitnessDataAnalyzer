using System;
using System.Collections.Generic;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.ViewModel
{
   public class ProgressViewModel : IProgressViewModel
   {
      public ProgressViewModel()
      {
         DataPoints = new Dictionary<DateTime, IDataPoint>();
         LowActivityDataPoints = new Dictionary<DateTime, IDataPoint>();
         HighActivityDataPoints = new Dictionary<DateTime, IDataPoint>();
         Categories = new Dictionary<string, ICategory>();
      }

      public IDictionary<DateTime, IDataPoint> DataPoints { get; }

      public IDictionary<DateTime, IDataPoint> LowActivityDataPoints { get; }

      public IDictionary<DateTime, IDataPoint> HighActivityDataPoints { get; }

      public IDictionary<string, ICategory> Categories { get; }
   }
}
