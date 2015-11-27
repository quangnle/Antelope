using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Models
{
    public class SignalRSubcriber: BaseSubcriber
    {
        public string Url { get; set; }

        public override string Name()
        {
            return "SignalR Subcriber";
        }
    }
}
