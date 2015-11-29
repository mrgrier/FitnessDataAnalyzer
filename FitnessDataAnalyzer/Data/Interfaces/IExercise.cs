using System;
using System.Collections.Generic;

namespace FitnessDataAnalyzer.Data.Interfaces
{
   public interface IExercise
   {
      string Name { get; }
      ICategory Category { get; }
      IDictionary<DateTime, ISet> Sets { get; }
   }
}
