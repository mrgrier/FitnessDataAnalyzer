using System;
using System.IO;
using System.Linq;
using FitnessDataAnalyzer.Data;
using FitnessDataAnalyzer.Data.Interfaces;
using FitnessDataAnalyzer.Helpers;
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
         m_viewModel.DataPoints = FileHelper.GetDataPointsFromFile(filePath);
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
               
               // get or add category.
               var categoryName = values[2];
               var category = m_viewModel.Categories.FirstOrDefault(c => c.Name.Equals(categoryName));
               if(category == null)
               {
                  category = new Category(categoryName);
                  m_viewModel.Categories.Add(category);
               }

               // get or add exercise.
               var exerciseName = values[1];
               IExercise exercise;
               if(!m_viewModel.Exercises.TryGetValue(category, out exercise))
               {
                  exercise = new Exercise(exerciseName, category);
                  m_viewModel.Exercises[category] = exercise;
               }

               var date = DateTime.Parse(values[0]);

               if(!string.IsNullOrEmpty(values[5]) && 
                  !string.IsNullOrEmpty(values[6]) &&
                  !string.IsNullOrEmpty(values[7]))
               {
                  // this is a distance exercise
                  var distance = double.Parse(values[5]);
                  var distanceUnit = ParseDistanceUnit(values[6]);
                  var duration = TimeSpan.Parse(values[7]);

                  var distanceSet = new DistanceSet(exercise, date, distance, distanceUnit, duration);

                  // TODO: add to view model.
                  continue;
               }

               if(!string.IsNullOrEmpty(values[3]) && !string.IsNullOrEmpty(values[4]))
               {
                  // this is a weighted exercise
                  var weight = double.Parse(values[3]);
                  var reps = int.Parse(values[4]);

                  var weightedSet = new WeightedSet(exercise, date, weight, reps);

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
