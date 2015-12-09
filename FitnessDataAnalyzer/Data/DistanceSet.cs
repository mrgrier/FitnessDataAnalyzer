using System;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.Data
{
   public class DistanceSet : IDistanceSet
   {
      public DistanceSet(DateTime date,
                         double distance, 
                         DistanceUnit unit, 
                         TimeSpan duration)
      {
         Date = date;
         Distance = distance;
         Unit = unit;
         Duration = duration;
      }

      public DateTime Date { get; }
      public double Distance { get; }
      public DistanceUnit Unit { get; }
      public TimeSpan Duration { get; }
   }
}
