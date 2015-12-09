using System.Collections.Generic;
using FitnessDataAnalyzer.Data;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.Extensions
{
   public static class EnumerableExtensions
   {
      public static IEnumerable<TSource> SkipEvery<TSource>(this IEnumerable<TSource> x, int skipCount)
      {
         var result = new List<TSource>();
         var skippedThisChunk = skipCount;

         foreach(var item in x)
         {
            if(skippedThisChunk == skipCount)
            {
               result.Add(item);
               skippedThisChunk = 0;
            }
            else
            {
               skippedThisChunk++;
            }
         }

         return result;
      }

      public static IEnumerable<IDataPoint> Smooth(this IEnumerable<IDataPoint> x,
                                                   int chunkSize)
      {
         if(chunkSize < 1)
         {
            return new List<IDataPoint>();
         }

         var result = new List<IDataPoint>();
         var chunked = 0;

         double caloriesSum = 0;
         double perspirationSum = 0;
         double heartRateSum = 0;
         double skinTempSum = 0;
         var stepsSum = 0;

         foreach(var item in x)
         {
            if(chunked == chunkSize)
            {
               result.Add(new DataPoint(item.Date, 
                                        caloriesSum / chunkSize,
                                        perspirationSum / chunkSize,
                                        heartRateSum / chunkSize,
                                        skinTempSum / chunkSize,
                                        stepsSum / chunkSize));

               chunked = 0;
               caloriesSum = perspirationSum = heartRateSum = skinTempSum = stepsSum = 0;
            }
            else
            {
               caloriesSum += item.Calories;
               perspirationSum += item.Perspiration;
               heartRateSum += item.HeartRate;
               skinTempSum += item.SkinTemp;
               stepsSum += item.Steps;

               chunked++;
            }
         }

         return result;
      }
   }
}
