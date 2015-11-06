using System;

namespace FitnessDataAnalyzer.Data.Interfaces
{
   internal interface IDistanceSet : ISet
   {
      double Distance { get; }
      DistanceUnit Unit { get; }
      TimeSpan Duration { get; }
   }
}
