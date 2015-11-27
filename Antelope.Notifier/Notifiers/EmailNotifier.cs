using Antelope.Notifier.Exceptions;
using Antelope.Notifier.Models;
using NLog;
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
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private string _emailAddr;
        private string _emailPassword;
        private string _emailDisplayName;
        private SmtpClient _smtp;

        public bool EnableSsl { get; set; }

        public abstract string SmtpHost();
        public abstract int SmtpPort();
        public abstract int SmtpSslPort();

        protected void Init(string emailAddr, string password, string emailDisplayName)
        {
            _emailAddr = emailAddr;
            _emailPassword = password;
            _emailDisplayName = emailDisplayName;
            EnableSsl = true;

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
            return string.Format("Email Notifier {0} <{1}>", _emailDisplayName, _emailAddr);
        }

        public void Notify(BaseSubcriber subcriber, BaseNotifierData data)
        {
            var emailSubcriber = subcriber as EmailSubcriber;
            var emailData = data as EmailNotifierData;

            if(emailData == null || emailSubcriber == null)
                throw new AntelopeInvalidParameter();

            try
            {
                var fromAddress = new MailAddress(_emailAddr, _emailDisplayName);
                var toAddress = new MailAddress(emailSubcriber.Email, emailSubcriber.DisplayName);

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = emailData.Title,
                    Body = emailData.Content
                })
                {
                    _smtp.Send(message);
                }
            }
            catch(FormatException)
            {
                throw new AntelopeInvalidEmailFormat();
            }
            catch(SmtpException ex)
            {
                throw new AntelopeSmtpException(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.Error("Caught exception when sending notification through email to {0}: {1}", emailSubcriber.Email, ex.ToString());
            }
        }
    }
}
