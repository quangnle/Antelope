using Antelope.Notifier.Models;
using Antelope.Notifier.Notifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier
{
    public class AntelopeObserver
    {
        private Dictionary<int, INotifier> _notifiers = new Dictionary<int, INotifier>();

        private Dictionary<int, List<BaseSubcriber>> _subcribers = new Dictionary<int, List<BaseSubcriber>>();

        public void AddNotifier(int notifierId, INotifier notifier)
        {
            _notifiers[notifierId] = notifier;
        }

        public void Register(int notifierId, BaseSubcriber subcriber)
        {
            if (!_subcribers.ContainsKey(notifierId))
            {
                _subcribers[notifierId] = new List<BaseSubcriber>();
            }

            _subcribers[notifierId].Add(subcriber);
        }

        public void Notify(int notifierId, BaseNotifierData data)
        {
            var notifier = _notifiers[notifierId];

            foreach (var subcriber in _subcribers[notifierId])
            {
                notifier.Notify(subcriber, data);
            }
        }

        public void NotifyAll(BaseNotifierData data)
        {
            foreach (var notifierId in _subcribers.Keys)
            {
                Notify(notifierId, data);
            }
        }
    }
}
