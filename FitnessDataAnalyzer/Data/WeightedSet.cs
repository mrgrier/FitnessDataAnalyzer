using System;

namespace FitnessDataAnalyzer.Data
{
   public class WeightedSet : IWeightedSet
   {
      public WeightedSet(string name, 
                         ICategory category,
                         DateTime date, 
                         double weight, 
                         int reps)
      {
         Name = name;
         Category = category;
         Date = date;
         Weight = weight;
         Reps = reps;
      }

      public string Name { get; }
      public ICategory Category { get; }
      public DateTime Date { get; }
      public double Weight { get; }
      public int Reps { get; }
   }
}
