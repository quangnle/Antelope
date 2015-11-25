using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.NotifiedData
{
    public class SkypeNotifiedData: BaseNotifiedData
    {
        public string ToAccountHandle { get; set; }
        public string Message { get; set; }
    }
}
