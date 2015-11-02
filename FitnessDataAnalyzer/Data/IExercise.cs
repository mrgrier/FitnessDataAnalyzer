using System;

namespace FitnessDataAnalyzer.Data
{
   public interface IExercise
   {
      string Name { get; }
      ICategory Category { get; }
      DateTime Date { get; }
   }
}
