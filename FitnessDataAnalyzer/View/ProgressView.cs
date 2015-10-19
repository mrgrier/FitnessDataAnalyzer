using System.Windows.Forms;
using FitnessDataAnalyzer.Presenter;

namespace FitnessDataAnalyzer.View
{
    public partial class ProgressView : Form, IProgressView
    {
        public ProgressView()
        {
            InitializeComponent();
            m_presenter = new ProgressPresenter(this);
        }

        private readonly IProgressPresenter m_presenter;
    }
}
