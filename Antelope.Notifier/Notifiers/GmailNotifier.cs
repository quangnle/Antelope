using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Notifiers
{
    public class GmailNotifier: EmailNotifier
    {
        public static GmailNotifier CreateNotifier(string emailAddr, string password, string emailDisplayName)            
        {
            var notifier = new GmailNotifier();
            notifier.Init(emailAddr, password, emailDisplayName);
            return notifier;
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
