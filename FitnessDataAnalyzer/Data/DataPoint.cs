using System;

namespace FitnessDataAnalyzer.Data
{
   public class DataPoint : IDataPoint
   {
      public DataPoint(DateTime date,
                       double calories,
                       double perspiration,
                       double heartRate,
                       double skinTemp,
                       int steps)
      {
         Date = date;
         Calories = calories;
         Perspiration = perspiration;
         HeartRate = heartRate;
         SkinTemp = skinTemp;
         Steps = steps;
      }

      public DateTime Date { get; }
      public double Calories { get; }
      public double Perspiration { get; }
      public double HeartRate { get; }
      public double SkinTemp { get; }
      public int Steps { get; }
   }
}
