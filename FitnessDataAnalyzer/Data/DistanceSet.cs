using System;

namespace FitnessDataAnalyzer.Data
{
   public class DistanceSet : IDistanceSet
   {
      public DistanceSet(string name, 
                         ICategory category,
                         DateTime date, 
                         double distance, 
                         DistanceUnit unit, 
                         TimeSpan duration)
      {
         Name = name;
         Category = category;
         Date = date;
         Distance = distance;
         Unit = unit;
         Duration = duration;
      }

      public string Name { get; }
      public ICategory Category { get; }
      public DateTime Date { get; }
      public double Distance { get; }
      public DistanceUnit Unit { get; }
      public TimeSpan Duration { get; }
   }
}
