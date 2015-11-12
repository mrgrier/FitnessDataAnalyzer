using System.Collections.Generic;

namespace FitnessDataAnalyzer.Extensions
{
   public static class EnumerableExtensions
   {
      public static IEnumerable<TItem> SkipEvery<TItem>(this IEnumerable<TItem> x, int skipCount)
      {
         var result = new List<TItem>();
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
   }
}
