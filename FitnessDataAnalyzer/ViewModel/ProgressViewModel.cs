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
         Categories = new List<ICategory>();
         Exercises = new Dictionary<ICategory, IExercise>();
      }

      public IDictionary<DateTime, IDataPoint> DataPoints { get; }

      public IDictionary<DateTime, IDataPoint> LowActivityDataPoints { get; }

      public IDictionary<DateTime, IDataPoint> HighActivityDataPoints { get; }

      public IList<ICategory> Categories { get; }

      public IDictionary<ICategory, IExercise> Exercises { get; }
   }
}
