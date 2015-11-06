using System;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.Data
{
   public class DistanceSet : IDistanceSet
   {
      public DistanceSet(IExercise exercise,
                         DateTime date,
                         double distance, 
                         DistanceUnit unit, 
                         TimeSpan duration)
      {
         Exercise = exercise;
         Date = date;
         Distance = distance;
         Unit = unit;
         Duration = duration;
      }

      public IExercise Exercise { get; }
      public DateTime Date { get; }
      public double Distance { get; }
      public DistanceUnit Unit { get; }
      public TimeSpan Duration { get; }
   }
}
