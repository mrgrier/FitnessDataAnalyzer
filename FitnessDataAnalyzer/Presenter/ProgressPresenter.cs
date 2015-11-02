using System;
using System.IO;
using System.Linq;
using FitnessDataAnalyzer.Data;
using FitnessDataAnalyzer.View;
using FitnessDataAnalyzer.ViewModel;

namespace FitnessDataAnalyzer.Presenter
{
   public class ProgressPresenter : IProgressPresenter
   {
      public ProgressPresenter(IProgressView view)
      {
         m_view = view;
         m_viewModel = new ProgressViewModel();
      }

      public void LoadDataPointsFromFile(string filePath)
      {
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

               m_viewModel.AddDataPoint(point);
            }
         }
      }

      public void LoadExerciseData(string filePath)
      {
         using(var reader = new StreamReader(File.OpenRead(filePath)))
         {
            while(!reader.EndOfStream)
            {
               var line = reader.ReadLine();
               if(line == null)
                  continue;

               var values = line.Split(',');

               var name = values[1];
               var date = DateTime.Parse(values[0]);

               if(!string.IsNullOrEmpty(values[5]) && 
                  !string.IsNullOrEmpty(values[6]) &&
                  !string.IsNullOrEmpty(values[7]))
               {
                  // this is a distance exercise
                  var distance = double.Parse(values[5]);
                  var distanceUnit = ParseDistanceUnit(values[6]);
                  var duration = TimeSpan.Parse(values[7]);

                  var category = m_viewModel.Categories.FirstOrDefault(c => c.Name.Equals(values[2]));
                  if(category == null)
                  {
                     category = new Category(values[2]);
                     m_viewModel.Categories.Add(category);
                  }

                  var distanceSet = new DistanceSet(name, 
                                                    category, 
                                                    date, 
                                                    distance, 
                                                    distanceUnit, 
                                                    duration);

                  // TODO: add to view model.
                  continue;
               }

               if(!string.IsNullOrEmpty(values[3]) && !string.IsNullOrEmpty(values[4]))
               {
                  // this is a weighted exercise
                  var weight = double.Parse(values[3]);
                  var reps = int.Parse(values[4]);
                  var category = m_viewModel.Categories.FirstOrDefault(c => c.Name.Equals(values[2]));
                  if(category == null)
                  {
                     category = new Category(values[2]);
                     m_viewModel.Categories.Add(category);
                  }

                  var weightedSet = new WeightedSet(name, category, date, weight, reps);

                  // TODO: add to view model.
                  continue;
               }

               throw new Exception("Entry did not match format for distance nor weighted exercise.");
            }
         }
      }

      private DistanceUnit ParseDistanceUnit(string unit)
      {
         if(unit == null)
            throw new ArgumentNullException(nameof(unit));

         if(unit.ToLower().Contains("mi"))
            return DistanceUnit.Miles;
         if(unit.ToLower().Contains("ki"))
            return DistanceUnit.Kilometers;
         
         // TODO: create a custom exception for this case.
         throw new Exception("Distance unit not recognized");
      }

      private readonly IProgressView m_view;
      private readonly IProgressViewModel m_viewModel;
   }
}
