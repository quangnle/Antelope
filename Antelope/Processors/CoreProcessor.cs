using Antelope.Data;
using Antelope.Data.Enums;
using Antelope.Data.Models;
using Antelope.Data.Repositories;
using Antelope.Data.ViewModels;
using Antelope.Notifier;
using Antelope.Notifier.Exceptions;
using Antelope.Notifier.Models;
using Antelope.Notifier.Notifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Antelope.Processors
{
    class CoreProcessor
    {
        private readonly int MilisecondsPerTick = 250;

        private MainModel _context;
        private bool _isRunning = false;

        private AccountRepository _accountRepository;

        public delegate void StartSuccessDelegate();
        public delegate void StopSuccessDelegate();
        public delegate void ChangeStatusDelegate(string status);

        public StartSuccessDelegate OnStartSuccess;
        public StopSuccessDelegate OnStopSuccess;
        public ChangeStatusDelegate OnUpdateStatus;

        public CoreProcessor(MainModel context)
        {
            _context = context;
            _accountRepository = new AccountRepository(context);
        }

        public void Start(AntelopeConfiguration config)
        {
            try
            {
                if (OnStartSuccess != null)
                    OnStartSuccess();

                _isRunning = true;

                while (_isRunning)
                {
                    // realtime database loading
                    UpdateStatus("Registering notifiers");
                    var notificationCenter = new AntelopeObserver();
                    notificationCenter.AddNotifier((int)ContactType.Email, GmailNotifier.CreateNotifier(config.Email, config.EmailPassword, config.EmailDisplayName));
                    notificationCenter.AddNotifier((int)ContactType.Skype, SkypeNotifier.CreateNotifier());

                    var contacts = _accountRepository.GetAllContacts();

                    foreach (var contact in contacts)
                    {
                        if (contact.ContactType == (int)ContactType.Email)
                            notificationCenter.Register((int)ContactType.Email, new EmailSubcriber() { Email = contact.Name, DisplayName = contact.Name });
                        else if (contact.ContactType == (int)ContactType.Skype)
                            notificationCenter.Register((int)ContactType.Skype, new SkypeSubcriber() { Handle = contact.Name });
                    }

                    UpdateStatus("Sending notification to operators");
                    var accounts = _accountRepository.GetAll();

                    foreach (var account in accounts)
                    {
                        if (account.NotifyThreshold <= account.Balance && account.Balance <= account.AutoActionThreshold)
                        {
                            SendNotification(notificationCenter, account);
                        }
                    }

                    CountDown(config.MonitoringPeriod);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Caught exception: {0}", ex.ToString()));
            }
            finally
            {
                if (OnStopSuccess != null)
                    OnStopSuccess();
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }

        private void SendNotification(AntelopeObserver notificationCenter, AccountViewModel account)
        {
            var content = string.Format("Account {3}/{0}/{1} with balance '{2}' exceeded the limit", 
                            account.Number, account.Name, account.Balance, account.BankName);

            notificationCenter.Notify((int)ContactType.Email,
                new EmailNotifierData()
                {
                    Title = string.Format("Antelope Notification - {0}", DateTime.Now.ToString()),
                    Content = content
                });

            notificationCenter.Notify((int)ContactType.Skype, new SkypeNotifierData() { Message = content });
        }

        private void UpdateStatus(string status)
        {
            if (OnUpdateStatus != null)
                OnUpdateStatus(status);
        }

        private void CountDown(int miliseconds)
        {
            var now = DateTime.Now;

            var bStopCountingDown = false;

            while (!bStopCountingDown)
            {
                var dt = DateTime.Now.Subtract(now).TotalMilliseconds;
                if (!_isRunning || dt >= miliseconds)
                    bStopCountingDown = true;
                else
                {
                    UpdateStatus(string.Format("Waiting {0} seconds to wake up", (int)((miliseconds - dt) / 1000)));
                    Thread.Sleep(MilisecondsPerTick);
                }
            }
        }
    }
}
