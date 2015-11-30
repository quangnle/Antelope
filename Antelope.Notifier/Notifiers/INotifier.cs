using Antelope.Notifier.Models;
using Antelope.Notifier.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Notifiers
{
    public interface INotifier
    {
        string Name { get; }
        IFormatter Formatter { get; }
        void Notify(ISubcriber subcriber, BaseNotifierData data);
    }
}
