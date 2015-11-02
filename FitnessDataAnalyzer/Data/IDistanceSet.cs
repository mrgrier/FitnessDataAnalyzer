using System;

namespace FitnessDataAnalyzer.Data
{
   public interface IDistanceSet : IExercise
   {
      double Distance { get; }
      DistanceUnit Unit { get; }
      TimeSpan Duration { get; }
   }
}
