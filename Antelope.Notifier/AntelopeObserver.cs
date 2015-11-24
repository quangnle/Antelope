using Antelope.Notifier.Notifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier
{
    class AntelopeObserver
    {
        private List<INotifier> _subcribers = new List<INotifier>();

        public void Register(INotifier sub)
        {
            _subcribers.Add(sub);
        }

        public void NotifyAll()
        {
            foreach (var sub in _subcribers)
            {
                //sub.Notify();
            }
        }
    }
}
