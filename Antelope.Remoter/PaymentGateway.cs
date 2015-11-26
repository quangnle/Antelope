using Antelope.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Remoter
{
    public class PaymentGateway
    {
        private MainModel _context = new MainModel();

        public void AutoTransferMoney(int idAccount, double amount)
        {
            var targetConfig = _context.TargetConfigs.Where(tc => tc.IdAccount == idAccount).FirstOrDefault();
            if (targetConfig != null)
                TransferMoney(idAccount, targetConfig.IdTarget, amount);
        }

        public void DepositMoneyToAccounts(double amount)
        {
            var accounts = (from acc in _context.Accounts
                            join tc in _context.TargetConfigs on acc.Id equals tc.IdAccount
                            select acc);

            foreach (var account in accounts)
            {
                account.Balance += amount;
            }
            _context.SaveChanges();
        }

        private void TransferMoney(int fromAccId, int toAccId, double amount)
        {
            using(var dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var fromAcc = _context.Accounts.FirstOrDefault(acc => acc.Id == fromAccId);
                    var toAcc = _context.Accounts.FirstOrDefault(acc => acc.Id == toAccId);

                    fromAcc.Balance -= amount;
                    toAcc.Balance += amount;

                    var log = new ExceedIncident();
                    log.DetectedAt = DateTime.Now;
                    log.IdAccount = fromAccId;
                    _context.ExceedIncidents.Add(log);
                    _context.SaveChanges();

                    var action = new Antelope.Data.Models.Action();
                    action.IdExceedIncident = log.Id;
                    action.ActionTime = DateTime.Now;
                    action.Description = string.Format("transfer {0} to account {1}/{2}", amount, toAcc.Number, toAcc.Name);
                    action.IdAccount = fromAccId;
                    action.Balance = fromAcc.Balance;
                    _context.Actions.Add(action);
                    _context.SaveChanges();

                    dbTransaction.Commit();
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                }
            }
            
        }

        private void DepositMoney(int idAccount, double amount)
        {
            var account = _context.Accounts.FirstOrDefault(acc => acc.Id == idAccount);
            account.Balance += amount;
            _context.SaveChanges();
        }
    }
}
