using System;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.Data
{
   public class WeightedSet : IWeightedSet
   {
      public WeightedSet(IExercise exercise,
                         DateTime date, 
                         double weight, 
                         int reps)
      {
         Exercise = exercise;
         Date = date;
         Weight = weight;
         Reps = reps;
      }
      
      public IExercise Exercise { get; }
      public DateTime Date { get; }
      public double Weight { get; }
      public int Reps { get; }
   }
}
