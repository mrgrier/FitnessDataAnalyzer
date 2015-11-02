namespace FitnessDataAnalyzer.Data
{
   public interface IWeightedSet : IExercise
   {
      double Weight { get; }
      int Reps { get; }
   }
}
