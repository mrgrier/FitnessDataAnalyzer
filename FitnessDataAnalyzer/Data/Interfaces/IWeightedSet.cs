namespace FitnessDataAnalyzer.Data.Interfaces
{
   internal interface IWeightedSet : ISet
   {
      double Weight { get; }
      int Reps { get; }
   }
}
