using System;

namespace FitnessDataAnalyzer.Data.Interfaces
{
   internal interface ISet
   {
      IExercise Exercise { get; }
      DateTime Date { get; }
   }
}
