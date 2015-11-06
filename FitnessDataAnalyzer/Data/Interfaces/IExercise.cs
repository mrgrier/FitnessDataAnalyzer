namespace FitnessDataAnalyzer.Data.Interfaces
{
   public interface IExercise
   {
      string Name { get; }
      ICategory Category { get; }
   }
}
