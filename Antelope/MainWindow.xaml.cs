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

            var processor = new CoreProcessor(context);
            Task.Run(() => processor.Start(config));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
