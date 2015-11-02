namespace FitnessDataAnalyzer.Data
{
   public class Category : ICategory
   {
      public Category(string name)
      {
         Name = name;
      }

      public string Name { get; }
   }
}
