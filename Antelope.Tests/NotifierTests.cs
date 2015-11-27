using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Antelope.Notifier.Notifiers;
using Antelope.Notifier.Exceptions;
using Antelope.Notifier.Models;
using Antelope.Notifier;
using Moq;

namespace Antelope.Tests
{
    [TestClass]
    public class NotifierTests
    {
        private static readonly string ANTELOPE_EMAIL = "aquoz.antelope@gmail.com";
        private static readonly string ANTELOPE_PASSWORD = "tancong1234";
        private static readonly string ANTELOPE_DISPLAYNAME = "Aquoz Antelope";

        private static EmailNotifier AntelopeEmailNotifier = 
            GmailNotifier.CreateNotifier(ANTELOPE_EMAIL, ANTELOPE_PASSWORD, ANTELOPE_DISPLAYNAME);

        private static EmailSubcriber AntelopeEmailSubcriber = new EmailSubcriber()
        {
            Email = ANTELOPE_EMAIL,
            DisplayName = ANTELOPE_DISPLAYNAME
        };

        private static EmailSubcriber InvalidEmailSubcriber = new EmailSubcriber()
        {
            Email = "foo",
            DisplayName = "foo"
        };

        private static EmailNotifierData EmailData = new EmailNotifierData()
        {
            Title = "title",
            Content = "content"
        };

        [TestMethod]
        public void Notifier_Gmail_Notifier()
        {
            var notifier = GmailNotifier.CreateNotifier(ANTELOPE_EMAIL, ANTELOPE_PASSWORD, ANTELOPE_DISPLAYNAME);
            var subcriber = new EmailSubcriber()
            {
                Email = ANTELOPE_EMAIL,
                DisplayName = ANTELOPE_DISPLAYNAME
            };

            notifier.Notify(subcriber, EmailData);
        }

        [TestMethod]
        [ExpectedException(typeof(AntelopeInvalidEmailFormat))]
        public void Notifier_Gmail_InvalidFromEmail()
        {
            var notifier = GmailNotifier.CreateNotifier("foo", "foo", "foo");
            notifier.Notify(AntelopeEmailSubcriber, EmailData);
        }

        [TestMethod]
        [ExpectedException(typeof(AntelopeInvalidEmailFormat))]
        public void Notifier_Gmail_InvalidToEmail()
        {
            AntelopeEmailNotifier.Notify(InvalidEmailSubcriber, EmailData);
        }

        [TestMethod]
        [ExpectedException(typeof(AntelopeSmtpException))]
        public void Notifier_Gmail_FailedAuthentication()
        {
            var notifier = GmailNotifier.CreateNotifier(ANTELOPE_EMAIL, "this is not password", ANTELOPE_DISPLAYNAME);
            notifier.Notify(AntelopeEmailSubcriber, EmailData);
        }

        [TestMethod]
        [ExpectedException(typeof(AntelopeInvalidParameter))]
        public void Notifier_GmailNotifierInvalidData()
        {
            var notifier = GmailNotifier.CreateNotifier("foo", "foo", "foo");
            notifier.Notify(new EmailSubcriber(), new SkypeNotifierData());
        }

        [TestMethod]
        public void Notifier_SkypeNotifierSuccessful()
        {
            var notifier = new SkypeNotifier();
            notifier.AttachToSkype();
            notifier.Notify(
                new SkypeSubcriber() { Handle = "legoslight" },
                new SkypeNotifierData() { Message = "antelope - message" });
        }

        [TestMethod]
        [ExpectedException(typeof(AntelopeInvalidParameter))]
        public void Notifier_SkypeNotifierInvalidData()
        {
            var notifier = new SkypeNotifier();
            notifier.Notify(new EmailSubcriber(), new EmailNotifierData());
        }
        
        [TestMethod]
        [ExpectedException(typeof(AntelopeUnknownTarget))]
        public void Notifier_SkypeNotifierUnknownTarget()
        {
            var notifier = new SkypeNotifier();
            notifier.AttachToSkype();
            notifier.Notify(
                new SkypeSubcriber() { Handle = "anonymous" },
                new SkypeNotifierData() { Message = "send me" });
        }

        [TestMethod]
        public void Notifier_SignalR_TestConnection()
        {
            var notifier = SignalRNotifier.CreateNotifier();
            notifier.Notify(
                new SignalRSubcriber() { Url = "http://localhost:8081/signalr" },
                new MessageNotifierData() { Content = "notify" });
        }
    }
}
