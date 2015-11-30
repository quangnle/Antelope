using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Notifiers
{
    public class AntelopeWebNotifier: SignalRNotifier
    {
        public static AntelopeWebNotifier CreateNotifier()
        {
            var notifier = new AntelopeWebNotifier();
            return notifier;
        }

        public override string HubName()
        {
            return "MyHub";
        }

        public override string HubMethod()
        {
            return "Send";
        }
    }
}
