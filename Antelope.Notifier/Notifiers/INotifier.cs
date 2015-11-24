using Antelope.Notifier.NotifiedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Notifiers
{
    public interface INotifier
    {
        string Name();
        void Notify(BaseNotifiedData data);
    }
}
