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
        private Dictionary<Type, SubcriberGroup> _subcribers = new Dictionary<Type, SubcriberGroup>();

        public void Register(Type notificationType, BaseNotifiedData data)
        {
            if (!_subcribers.ContainsKey(notificationType))
            {
                _subcribers[notificationType] = new SubcriberGroup()
                {
                    Notifier = Activator.CreateInstance(notificationType) as INotifier,
                    Data = new List<BaseNotifiedData>()
                };
            }

            _subcribers[notificationType].Data.Add(data);
        }

        public void NotifyAll()
        {
            foreach (var sub in _subcribers)
            {
                var subGroup = sub.Value;

                foreach (var data in subGroup.Data)
                {
                    subGroup.Notifier.Notify(data);
                }
            }
        }
    }
}
