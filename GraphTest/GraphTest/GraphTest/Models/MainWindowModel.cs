using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ZGroup.MonitoringSuite.BDO;
using ZGroup.MonitoringSuite.BDO.Client;

namespace GraphTest.Models
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private ObservableCollection<GasDetectorFake> gasDetectors;

        public ObservableCollection<GasDetectorFake> GasDetectors
        {
            get { return gasDetectors; }
            set
            {
                gasDetectors = value;
                OnPropertyChanged("GasDetectors");
            }
        }


        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion

    }

    public class GasDetectorFake
    {
        public long GasDetectorId { get; set; }
        public decimal Value { get; set; }
    }
}
