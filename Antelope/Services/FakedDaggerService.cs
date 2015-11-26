using Antelope.Remoter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Antelope.Services
{
    public class FakedDaggerService
    {
        public void Start()
        {
            var gateway = new PaymentGateway();

            while (true)
            {
                gateway.DepositMoneyToAccounts(5);

                Thread.Sleep(1000);
            }
        }
    }
}
