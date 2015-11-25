using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Data.Views
{
    class AccountView
    {
        public int Id { get; set; }
        public int AccountType { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public double Balance { get; set; }
        public int Status { get; set; }
        public double NotifyThreshold { get; set; }
        public double AutoActionThreshold { get; set; }
        public DateTime StartEffectiveDate { get; set; }
        public DateTime EndEffectiveDate { get; set; }
        public int NumberOfRetries { get; set; }
        public int MonitorPeriod { get; set; }
    }
}
