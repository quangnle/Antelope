using Antelope.Notifier.Models;
using Antelope.Notifier.Notifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier
{
    public class SubcriberGroup
    {
        public INotifier Notifier { get; set; }
        public List<BaseNotifierData> Data { get; set; }
    }
}
