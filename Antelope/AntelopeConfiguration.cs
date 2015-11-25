using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope
{
    class AntelopeConfiguration
    {
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string EmailDisplayName { get; set; }
        public int MonitoringPeriod { get; set; }
    }
}
