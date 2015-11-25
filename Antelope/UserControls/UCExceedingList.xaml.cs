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
using Antelope.Data;
using Antelope.Data.Models;

namespace Antelope.UserControls
{
    /// <summary>
    /// Interaction logic for UCExceedingList.xaml
    /// </summary>
    public partial class UCExceedingList : UserControl
    {
        public List<Account> DataSource { get; set; }

        public UCExceedingList()
        {
            InitializeComponent();
        }
    }
}
