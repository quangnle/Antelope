using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Models
{
    public class SignalRSubcriber: ISubcriber
    {
        public string Url { get; set; }

        public string Name
        {
            get { return "SignalR Subcriber"; }
        }
    }
}
