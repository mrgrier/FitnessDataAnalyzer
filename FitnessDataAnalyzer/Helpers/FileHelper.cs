using System;
using System.Collections.Generic;
using System.IO;
using FitnessDataAnalyzer.Data;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.Helpers
{
   public static class FileHelper
   {
      public static IDictionary<DateTime, IDataPoint> GetDataPointsFromFile(string filePath)
      {
         var dataPoints = new Dictionary<DateTime, IDataPoint>();

         using(var reader = new StreamReader(File.OpenRead(filePath)))
         {
            while(!reader.EndOfStream)
            {
               var line = reader.ReadLine();
               if(line == null)
                  continue;

               var values = line.Split(',');

               var point = new DataPoint(DateTime.Parse(values[0]),
                                         double.Parse(values[1]),
                                         double.Parse(values[2]),
                                         double.Parse(values[3]),
                                         double.Parse(values[4]),
                                         int.Parse(values[5]));

               dataPoints[point.Date] = point;
            }
         }

         return dataPoints;
      }
   }
}
