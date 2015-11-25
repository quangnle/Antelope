using Antelope.Notifier.Exceptions;
using Antelope.Notifier.Models;
using Antelope.Notifier.NotifiedData;
using SKYPE4COMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Notifiers
{
    public class SkypeNotifier : INotifier
    {
        private Skype _skypeHandler;
        private List<SkypeUser> _friends; 

        public bool AttachToSkype()
        {
            _skypeHandler = new Skype();
            _skypeHandler.Attach(7, false);

            if (!_skypeHandler.Client.IsRunning)
                return false;

            _friends = new List<SkypeUser>();

            foreach (var usr in _skypeHandler.Friends.OfType<User>())
            {
                _friends.Add(new SkypeUser() { 
                    Handle = usr.Handle, 
                    Aliases = usr.Aliases, 
                    DisplayName = usr.DisplayName 
                });
            }

            return true;
        }

        public string Name()
        {
            return "SkypeNotifier";
        }

        public void Notify(BaseNotifiedData data)
        {
            var skypeData = data as SkypeNotifiedData;

            if (skypeData == null)
                throw new AntelopeInvalidNotifiedData();

            if (_friends.Find(fr => fr.Handle == skypeData.ToAccountHandle) == null)
                throw new AntelopeUnknownTarget();

            _skypeHandler.SendMessage(skypeData.ToAccountHandle, skypeData.Message);
        }
    }
}
