using Antelope.Data;
using Antelope.Data.Enums;
using Antelope.Data.Models;
using Antelope.Data.Repositories;
using Antelope.Data.ViewModels;
using Antelope.Notifier;
using Antelope.Notifier.Exceptions;
using Antelope.Notifier.Models;
using Antelope.Notifier.Notifiers;
using Antelope.Remoter;
using NLog;
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
        private static Logger _logger = LogManager.GetCurrentClassLogger();

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
                    notificationCenter.AddNotifier((int)ContactType.SignalrBasedWebsite, SignalRNotifier.CreateNotifier());

                    var contacts = _accountRepository.GetAllContacts();

                    foreach (var contact in contacts)
                    {
                        if (contact.ContactType == (int)ContactType.Email)
                            notificationCenter.Register((int)ContactType.Email, new EmailSubcriber() { Email = contact.Name, DisplayName = contact.Name });
                        else if (contact.ContactType == (int)ContactType.Skype)
                            notificationCenter.Register((int)ContactType.Skype, new SkypeSubcriber() { Handle = contact.Name });
                        else if (contact.ContactType == (int)ContactType.SignalrBasedWebsite)
                            notificationCenter.Register((int)ContactType.SignalrBasedWebsite, new SignalRSubcriber() { Url = contact.Name });
                    }

                    UpdateStatus("Sending notification to operators");
                    var accounts = _accountRepository.GetAll();

                    var exceedNotificationLimitAccounts = accounts.Where(acc => acc.NotifyThreshold <= acc.Balance && acc.Balance <= acc.AutoActionThreshold).ToList();
                    var exceedLimitAccounts = accounts.Where(acc => acc.Balance >= acc.AutoActionThreshold).ToList();

                    SendNotification(notificationCenter, exceedNotificationLimitAccounts);
                    AutoTransferMoney(exceedLimitAccounts);

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

        private void SendNotification(AntelopeObserver notificationCenter, List<AccountViewModel> accounts)
        {
            if (accounts.Count == 0)
                return;

            var content = string.Join("\n",
                accounts.Select((account, idx) =>
                    string.Format("{0}. Account {1}/{2}/{3} with balance '{4}' exceeded the limit '{5}'",
                            (idx + 1), // {0}
                            account.BankName, // {1}
                            account.Number, // {2}
                            account.Name, // {3}
                            account.Balance, // {4}
                            account.NotifyThreshold // {5}
                    )));

            notificationCenter.Notify((int)ContactType.Email,
                new EmailNotifierData()
                {
                    Title = string.Format("Antelope Notification - {0} accounts exceeded the balance", accounts.Count),
                    Content = content
                });

            notificationCenter.Notify((int)ContactType.Skype, new SkypeNotifierData() { Message = content });

            notificationCenter.Notify((int)ContactType.SignalrBasedWebsite, new MessageNotifierData() { Content = content });
        }

        private void AutoTransferMoney(List<AccountViewModel> accounts)
        {
            PaymentGateway gateway = new PaymentGateway();

            foreach (var account in accounts)
            {
                gateway.AutoTransferMoney(account.Id, account.AutoActionThreshold);
            }
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
