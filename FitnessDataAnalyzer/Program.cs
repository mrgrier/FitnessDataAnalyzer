using System;
using System.Windows.Forms;
using FitnessDataAnalyzer.View;

namespace FitnessDataAnalyzer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProgressView());
        }
    }
}
