using Antelope.Notifier.NotifiedData;
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
        private Dictionary<int, INotifier> _notifiers = new Dictionary<int, INotifier>();

        private Dictionary<int, List<BaseNotifiedData>> _subcribers = new Dictionary<int, List<BaseNotifiedData>>();

        public void AddNotifier(int notifierId, INotifier notifier)
        {
            _notifiers[notifierId] = notifier;
        }

        public void Register(int notifierId, BaseNotifiedData data)
        {
            if (!_subcribers.ContainsKey(notifierId))
            {
                _subcribers[notifierId] = new List<BaseNotifiedData>();
            }

            _subcribers[notifierId].Add(data);
        }

        public void NotifyAll()
        {
            foreach (var notifierId in _subcribers.Keys)
            {
                var notifier = _notifiers[notifierId];

                var notifiedData = _subcribers[notifierId];

                foreach (var data in notifiedData)
                {
                    notifier.Notify(data);
                }
            }
        }
    }
}
