using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FitnessDataAnalyzer.Annotations;
using FitnessDataAnalyzer.Data.Interfaces;

namespace FitnessDataAnalyzer.ViewModel
{
   public sealed class ProgressViewModel : IProgressViewModel, INotifyPropertyChanged
   {
      public ProgressViewModel()
      {
         DataPoints = new Dictionary<DateTime, IDataPoint>();
         LowActivityDataPoints = new Dictionary<DateTime, IDataPoint>();
         HighActivityDataPoints = new Dictionary<DateTime, IDataPoint>();
         Categories = new Dictionary<string, ICategory>();

         m_watchDataLoaded = true;
         m_setDataLoaded = true;
      }

      public IDictionary<DateTime, IDataPoint> DataPoints { get; }

      public IDictionary<DateTime, IDataPoint> LowActivityDataPoints { get; }

      public IDictionary<DateTime, IDataPoint> HighActivityDataPoints { get; }

      public IDictionary<string, ICategory> Categories { get; }

      public bool WatchDataNotYetLoaded
      {
         get { return m_watchDataLoaded; }
         set
         {
            if(m_watchDataLoaded == value) return;

            m_watchDataLoaded = value;
            OnPropertyChanged();
         }
      }

      public bool SetDataNotYetLoaded
      {
         get { return m_setDataLoaded; }
         set
         {
            if(m_setDataLoaded == value) return;

            m_setDataLoaded = value;
            OnPropertyChanged();
         }
      }

      public void Clear()
      {
         DataPoints.Clear();
         LowActivityDataPoints.Clear();
         HighActivityDataPoints.Clear();
         Categories.Clear();

         WatchDataNotYetLoaded = SetDataNotYetLoaded = true;
      }

      [NotifyPropertyChangedInvocator]
      private void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      public event PropertyChangedEventHandler PropertyChanged;

      private bool m_watchDataLoaded;
      private bool m_setDataLoaded;
   }
}
