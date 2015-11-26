using Antelope.Data;
using Antelope.Data.Models;
using Antelope.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Antelope
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CoreProcessor _processor;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var context = new MainModel();

            var config = new AntelopeConfiguration()
            {
                Email = txtEmail.Text,
                EmailPassword = txtPassword.Password,
                EmailDisplayName = txtDisplayName.Text,
                MonitoringPeriod = Convert.ToInt32(txtMonitoringPeriod.Text)
            };

            _processor = new CoreProcessor(context);
            _processor.OnStartSuccess += OnStartSuccess;
            _processor.OnStopSuccess += OnStopSuccess;
            _processor.OnUpdateStatus += OnUpdateStatus;

            Task.Run(() => _processor.Start(config));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _processor.Stop();
        }

        private void OnStartSuccess()
        {
            Dispatcher.Invoke(() =>
            {
                btnStart.IsEnabled = false;
                btnStop.IsEnabled = true;
                OnUpdateStatus("Started");
            });
        }

        private void OnStopSuccess()
        {
            Dispatcher.Invoke(() =>
            {
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;
                OnUpdateStatus("Stopped");
            });
        }

        private void OnUpdateStatus(string status)
        {
            Dispatcher.Invoke(() =>
            {
                labelStatus.Content = status;
            });
        }
    }
}
