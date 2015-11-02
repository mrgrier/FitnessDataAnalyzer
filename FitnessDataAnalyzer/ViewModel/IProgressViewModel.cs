using System;
using System.Collections.Generic;
using FitnessDataAnalyzer.Data;

namespace FitnessDataAnalyzer.ViewModel
{
   internal interface IProgressViewModel
   {
      IDictionary<DateTime, IDataPoint> DataPoints { get; }
      IList<ICategory> Categories { get; }

      void AddDataPoint(IDataPoint point);
   }
}
