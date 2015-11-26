using Antelope.Data.Models;
using Antelope.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Data.Repositories
{
    public class AccountRepository
    {
        private MainModel _context;
        private IQueryable<AccountViewModel> _queryAll;

        public AccountRepository(MainModel context)
        {
            _context = context;
            _queryAll = (from acc in _context.Accounts
                         join ac in _context.AccountConfigs on acc.Id equals ac.IdAccount
                         join b in _context.Banks on acc.IdBank equals b.Id
                         select new AccountViewModel()
                         {
                             Id = acc.Id,
                             AccountType = acc.AccountType,
                             Name = acc.Name,
                             Number = acc.Number,
                             Balance = acc.Balance,
                             Status = acc.Status,
                             NotifyThreshold = ac.NotifyThreshold,
                             AutoActionThreshold = ac.AutoActionThreshold,
                             StartEffectiveDate = ac.StartEffectiveDate,
                             EndEffectiveDate = ac.EndEffectiveDate,
                             NumberOfRetries = ac.NumberOfRetries,
                             MonitorPeriod = ac.MonitorPeriod,
                             BankName = b.Name
                         });
        }

        public List<AccountViewModel> GetAll()
        {
            return _queryAll.ToList();
        }

        public List<AccountViewModel> GetMatchedAcc(bool c1,bool c2,bool c3)
        {
            return _queryAll.Where(acc => (c1 ? acc.Balance < acc.NotifyThreshold : false)
                || (c2 ? acc.Balance > acc.NotifyThreshold && acc.Balance < acc.AutoActionThreshold : false)
                || (c3 ? acc.Balance > acc.AutoActionThreshold : false)).ToList();
        }

        public List<Contact> GetAllContacts()
        {
            return _context.Contacts.ToList();
        }
    }
}
