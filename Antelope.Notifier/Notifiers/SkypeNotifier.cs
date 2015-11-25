using Antelope.Notifier.Exceptions;
using Antelope.Notifier.Models;
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

        public void Notify(BaseSubcriber subcriber, BaseNotifierData data)
        {
            var skypeData = data as SkypeNotifierData;
            var skypeSubcriber = subcriber as SkypeSubcriber;

            if (skypeData == null || skypeSubcriber == null)
                throw new AntelopeInvalidParameter();

            if (_friends.Find(fr => fr.Handle == skypeSubcriber.Handle) == null)
                throw new AntelopeUnknownTarget();

            _skypeHandler.SendMessage(skypeSubcriber.Handle, skypeData.Message);
        }
    }
}
