using System.Collections.Generic;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.Data
{
   public class Category : ICategory
   {
      public Category(string name)
      {
         Name = name;
         Exercises = new Dictionary<string, IExercise>();
      }

      public string Name { get; }
      public IDictionary<string, IExercise> Exercises { get; }
   }
}
