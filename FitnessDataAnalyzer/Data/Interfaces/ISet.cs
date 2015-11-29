using System;

namespace FitnessDataAnalyzer.Data.Interfaces
{
   public interface ISet
   {
      IExercise Exercise { get; }
      DateTime Date { get; }
   }
}
