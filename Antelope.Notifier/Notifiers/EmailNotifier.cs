using Antelope.Notifier.NotifiedData;
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
        private string _emailAddr;
        private string _emailPassword;
        private string _emailDisplayName;
        private SmtpClient _smtp;

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

            _smtp = new SmtpClient
            {
                Host = SmtpHost(),
                Port = EnableSsl ? SmtpSslPort() : SmtpPort(),
                EnableSsl = EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailAddr, _emailPassword)
            };
        }

        public string Name()
        {
            return "Email Notifier";
        }

        public void Notify(BaseNotifiedData data)
        {
            var emailData = data as EmailNotifiedData;

            if(emailData == null)
                throw new ArgumentNullException();

            var fromAddress = new MailAddress(_emailAddr, _emailDisplayName);
            var toAddress = new MailAddress(emailData.Email, emailData.DisplayedName);

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = emailData.Title,
                Body = emailData.Content
            })
            {
                _smtp.Send(message);
            }
        }
    }
}
