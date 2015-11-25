using Antelope.Data;
using Antelope.Data.Enums;
using Antelope.Data.Models;
using Antelope.Data.Repositories;
using Antelope.Data.ViewModels;
using Antelope.Notifier;
using Antelope.Notifier.Models;
using Antelope.Notifier.Notifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Antelope.Processors
{
    class CoreProcessor
    {
        private MainModel _context;
        private bool _isRunning = false;

        private AccountRepository _accountRepository;
        private AntelopeObserver _notifierCenter;

        public CoreProcessor(MainModel context)
        {
            _context = context;
            _accountRepository = new AccountRepository(context);
        }

        public void Start()
        {
            _notifierCenter = new AntelopeObserver();
            _notifierCenter.AddNotifier((int)ContactType.Email, new GmailNotifier("peterpan.hx@gmail.com", "Since!990", "Nguyen Tan Cong"));
            _notifierCenter.AddNotifier((int)ContactType.Skype, SkypeNotifier.CreateNotifier());

            var contacts = _accountRepository.GetAllContacts();

            foreach (var contact in contacts)
            {
                if (contact.ContactType == (int)ContactType.Email)
                    _notifierCenter.Register((int)ContactType.Email, new EmailSubcriber() { Email = contact.Name, DisplayName = contact.Name });
                else if (contact.ContactType == (int)ContactType.Skype)
                    _notifierCenter.Register((int)ContactType.Skype, new SkypeSubcriber() { Handle = contact.Name });
            }

            var mp = 3000;
            
            _isRunning = true;

            while(_isRunning)
            {
                var accounts = _accountRepository.GetAll();

                foreach (var account in accounts)
                {
                    if(account.NotifyThreshold <= account.Balance && account.Balance <= account.AutoActionThreshold)
                    {
                        SendNotification(account);
                    }
                }

                Thread.Sleep(mp);
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }

        private void SendNotification(AccountViewModel account)
        {
            var content = string.Format("Account {3}/{0}/{1} with balance '{2}' exceeded the limit", account.Number, account.Name, account.Balance, account.BankName);

            _notifierCenter.Notify((int)ContactType.Email,
                new EmailNotifierData()
                {
                    Title = string.Format("Antelope Notification - {0}", DateTime.Now.ToString()),
                    Content = content
                });

            _notifierCenter.Notify((int)ContactType.Skype, new SkypeNotifierData() { Message = content });
        }
    }
}
