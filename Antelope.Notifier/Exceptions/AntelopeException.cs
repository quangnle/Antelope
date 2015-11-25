using Antelope.Notifier.Notifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Exceptions
{
    public class AntelopeInvalidParameter : Exception { }
    public class AntelopeUnknownTarget : Exception { }
    public class AntelopeUninitializedNotifier : Exception { }
    public class AntelopeInvalidNotifier : Exception 
    {
        private INotifier _notifier;
        public AntelopeInvalidNotifier(INotifier notifier)
        {
            _notifier = notifier;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", "AntelopeInvalidNotifier", (_notifier != null)?_notifier.Name():"null");
        }
    }
}
