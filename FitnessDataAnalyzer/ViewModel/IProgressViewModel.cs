using System;
using System.Collections.Generic;
using FitnessDataAnalyzer.Data;

namespace FitnessDataAnalyzer.ViewModel
{
   interface IProgressViewModel
   {
      IDictionary<DateTime, IDataPoint> DataPoints { get; }

      void AddDataPoint(IDataPoint point);
   }
}
