using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Notifiers
{
    class GmailNotifier: EmailNotifier
    {
        public GmailNotifier(string emailAddr, string password, string emailDisplayName)
            :base(emailAddr, password, emailAddr)
        {

        }

        public override string SmtpHost()
        {
            return "smtp.gmail.com";
        }

        public override int SmtpPort()
        {
            return 25;
        }

        public override int SmtpSslPort()
        {
            return 587;
        }
    }
}
