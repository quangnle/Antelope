using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Notifiers
{
    public abstract class EmailNotifier : INotifier
    {
        private readonly string AntelopeEmail = "shenlongbk@gmail.com";
        private readonly string AntelopeName = "Antelope";
        private readonly string SubjectTemplate = "subject";
        private readonly string ContentTemplate = "content";

        private string _emailAddr;
        private string _emailPassword;
        private string _emailDisplayName;

        public bool EnableSsl { get; set; }

        public abstract string SmtpHost();
        public abstract int SmtpPort();
        public abstract int SmtpSslPort();

        public EmailNotifier(string emailAddr, string password, string emailDisplayName)
        {
            _emailAddr = emailAddr;
            _emailPassword = password;
            _emailDisplayName = emailDisplayName;

            EnableSsl = false;
        }

        public string Name()
        {
            return "Email Notifier";
        }

        public void Notify()
        {
            var fromAddress = new MailAddress(AntelopeEmail, AntelopeName);
            var toAddress = new MailAddress(_emailAddr, _emailDisplayName);

            var smtp = new SmtpClient
            {
                Host = SmtpHost(),
                Port = EnableSsl?SmtpSslPort():SmtpPort(),
                EnableSsl = EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, _emailPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = SubjectTemplate,
                Body = ContentTemplate
            })
            {
                smtp.Send(message);
            }
        }
    }
}
