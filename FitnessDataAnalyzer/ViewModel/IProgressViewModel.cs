using System;
using System.Collections.Generic;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.ViewModel
{
   public interface IProgressViewModel
   {
      IDictionary<DateTime, IDataPoint> DataPoints { get; }

      IDictionary<DateTime, IDataPoint> LowActivityDataPoints { get; }

      IDictionary<DateTime, IDataPoint> HighActivityDataPoints { get; }

      IDictionary<string, ICategory> Categories { get; }

      bool WatchDataNotYetLoaded { get; set; }

      bool SetDataNotYetLoaded { get; set; }
   }
}
