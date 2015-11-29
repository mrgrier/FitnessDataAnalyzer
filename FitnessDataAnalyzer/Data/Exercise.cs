using System;
using System.Collections.Generic;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.Data
{
   public class Exercise : IExercise
   {
      public Exercise(string name, ICategory category)
      {
         Name = name;
         Category = category;
         Sets = new Dictionary<DateTime, ISet>();
      }

      public string Name { get; }
      public ICategory Category { get; }
      public IDictionary<DateTime, ISet> Sets { get; }
   }
}
