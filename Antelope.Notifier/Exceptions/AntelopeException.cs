using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Exceptions
{
    public class AntelopeInvalidNotifiedData : Exception { }
    public class AntelopeUnknownTarget : Exception { }
    public class AntelopeUninitializedNotifier : Exception { }
}
