using System;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.Data
{
   public class WeightedSet : IWeightedSet
   {
      public WeightedSet(DateTime date, 
                         double weight, 
                         int reps)
      {
         Date = date;
         Weight = weight;
         Reps = reps;
      }
      
      public DateTime Date { get; }
      public double Weight { get; }
      public int Reps { get; }
   }
}
