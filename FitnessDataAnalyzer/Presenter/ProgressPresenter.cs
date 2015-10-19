using FitnessDataAnalyzer.View;

namespace FitnessDataAnalyzer.Presenter
{
   public class ProgressPresenter : IProgressPresenter
   {
      public ProgressPresenter(IProgressView view)
      {
         m_view = view;
      }

      private readonly IProgressView m_view;
   }
}
