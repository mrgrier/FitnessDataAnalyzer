using System;
using System.Collections.Generic;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.ViewModel
{
   internal interface IProgressViewModel
   {
      IDictionary<DateTime, IDataPoint> DataPoints { get; set; }
      IList<ICategory> Categories { get; }
      IDictionary<ICategory, IExercise> Exercises { get; }
   }
}
