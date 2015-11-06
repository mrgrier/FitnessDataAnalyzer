using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.Data
{
   public class Exercise : IExercise
   {
      public Exercise(string name, ICategory category)
      {
         Name = name;
         Category = category;
      }

      public string Name { get; }
      public ICategory Category { get; }
   }
}
