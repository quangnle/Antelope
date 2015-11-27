using Antelope.Notifier.Exceptions;
using Antelope.Notifier.Models;
using Antelope.Notifier.Notifiers;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier
{
    public class AntelopeObserver
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private Dictionary<int, INotifier> _notifiers = new Dictionary<int, INotifier>();

        private Dictionary<int, List<ISubcriber>> _subcribers = new Dictionary<int, List<ISubcriber>>();

        public void AddNotifier(int channel, INotifier notifier)
        {
            if (notifier == null)
                throw new AntelopeInvalidNotifier("notifier is null");

            _notifiers[channel] = notifier;
            _subcribers[channel] = new List<ISubcriber>();
        }

        public void Register(int channel, ISubcriber subcriber)
        {
            if (!ExistNotifier(channel))
                throw new AntelopeNotiferNotFound();

            _subcribers[channel].Add(subcriber);
        }

        public void Notify(int channel, BaseNotifierData data)
        {
            if (!ExistNotifier(channel) || !ExistSubrcibersOn(channel))
                return;

            var notifier = _notifiers[channel];

            foreach (var subcriber in _subcribers[channel])
            {
                try
                {
                    notifier.Notify(subcriber, data);
                }
                catch (Exception ex)
                {
                    _logger.Error("Caught Exception: {0}", ex.ToString());
                }
            }
        }

        public void NotifyAll(BaseNotifierData data)
        {
            foreach (var notifierId in _subcribers.Keys)
            {
                Notify(notifierId, data);
            }
        }

        private bool ExistNotifier(int channel)
        {
            return _notifiers.ContainsKey(channel);
        }

        private bool ExistSubrcibersOn(int channel)
        {
            return (_subcribers.ContainsKey(channel) && _subcribers[channel].Count > 0);
        }
    }
}
