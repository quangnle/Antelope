using Antelope.Notifier.NotifiedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Notifiers
{
    public class SkypeNotifier : INotifier
    {
        public string Name()
        {
            return "Skype Notifier";
        }

        public void Notify(BaseNotifiedData data)
        {
            throw new NotImplementedException();
        }
    }
}
