using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GraphTest.GasDetectorReaderService;
using GraphTest.Models;
using ZGroup.MonitoringSuite.BDO.Client;

namespace GraphTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowModel model = new MainWindowModel();

        private Task gasDetectorUpdateTask;
        private CancellationTokenSource gasDetectorCancellationTokenSource;
        private CancellationToken gasDetectorCancellationToken;

        private GasDetectorReaderService.GasDetectorReaderWcfServiceClient client;

        public MainWindow()
        {
            model.GasDetectors = new ObservableCollection<GasDetectorFake>();
            model.GasDetectors.Add(new GasDetectorFake() { GasDetectorId = 528 ,Value = 0});
            model.GasDetectors.Add(new GasDetectorFake() { GasDetectorId = 529, Value = 0 });
            model.GasDetectors.Add(new GasDetectorFake() { GasDetectorId = 530, Value = 0 });
            model.GasDetectors.Add(new GasDetectorFake() { GasDetectorId = 531, Value = 0 });
            model.GasDetectors.Add(new GasDetectorFake() { GasDetectorId = 532, Value = 0 });

            DataContext = model;
            InitializeComponent();
            client = new GasDetectorReaderWcfServiceClient();

            LoadData();
        }

        private void LoadData()
        {
            gasDetectorCancellationTokenSource = new CancellationTokenSource();
            gasDetectorCancellationToken = gasDetectorCancellationTokenSource.Token;
            gasDetectorUpdateTask = Task.Factory.StartNew(GetMeasurementData, gasDetectorCancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);

        }

        private void GetMeasurementData()
        {
            var app = ((App)Application.Current);
            if (app != null)
            {
                while (true)
                {
                    if (gasDetectorCancellationToken.IsCancellationRequested)
                    {
                        // Clean up here, then...
                        gasDetectorCancellationToken.ThrowIfCancellationRequested();
                    }
                    Parallel.ForEach(model.GasDetectors, detector =>
                    {
                        ZGroup.MonitoringSuite.BDO.Common.Measurement measurement = client.GetLastMeasurement(detector.GasDetectorId);
                        if (measurement != null)
                        {
                            Console.WriteLine("Received measurement for detector with ip {0}", detector.GasDetectorId);
                            Console.WriteLine("Value: {0}, FaultState: {1}, AlarmReached: {2}", measurement.Value, measurement.DeviceFault, measurement.AlarmReached);
                        }
                        else
                            Console.WriteLine("Measurement for detector with ip {0} is null!", detector.GasDetectorId);
                        if (measurement != null)
                        {
                            detector.Value = measurement.Value / 10;
                        }
                    });
                    //TODO:BDM: change to config value (interval gas measurements)
                    var sleepTime = new TimeSpan(0, 0, 0, 0, 800);
                    Thread.Sleep(sleepTime);
                }
            }
        }


        private void OpenGraphClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
