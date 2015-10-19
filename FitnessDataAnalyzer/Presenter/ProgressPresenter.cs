using System;
using System.IO;
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

      private readonly IProgressView m_view;
      private readonly IProgressViewModel m_viewModel;
   }
}
