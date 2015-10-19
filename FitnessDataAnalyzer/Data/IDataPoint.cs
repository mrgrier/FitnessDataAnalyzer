﻿using System;

namespace FitnessDataAnalyzer.Data
{
   public interface IDataPoint
   {
      DateTime Date { get; }
      double Calories { get; }
      double Perspiration { get; }
      double HeartRate { get; }
      double SkinTemp { get; }
      int Steps { get; }
   }
}
