using System;
using System.Collections.Generic;

namespace FitnessDataAnalyzer.Data.Interfaces
{
   public interface IExercise
   {
      string Name { get; }
      IDictionary<DateTime, ISet> Sets { get; }
   }
}
