using System.Collections.Generic;

namespace FitnessDataAnalyzer.Data.Interfaces
{
   public interface ICategory
   {
      string Name { get; }

      IDictionary<string, IExercise> Exercises { get; }
   }
}
