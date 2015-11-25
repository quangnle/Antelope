using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Antelope.Notifier.Notifiers;
using Antelope.Notifier.Exceptions;
using Antelope.Notifier.Models;

namespace Antelope.Tests
{
    [TestClass]
    public class NotifierTests
    {
        [TestMethod]
        public void TestGmailNotifier()
        {
            var notifier = new GmailNotifier("peterpan.hx@gmail.com", "Since!990", "Nguyen Tan Cong") { EnableSsl = true };
            var subcriber = new EmailSubcriber()
            {
                Email = "peterpan.hx@gmail.com",
                DisplayName = "Nguyen Tan Cong"
            };

            var data = new EmailNotifierData()
            {
                Title = "this is a test",
                Content = "this is a test body"
            };

            notifier.Notify(subcriber, data);
        }

        [TestMethod]
        [ExpectedException(typeof(AntelopeInvalidParameter))]
        public void GmailNotifierInvalidData()
        {
            var notifier = new GmailNotifier("foo", "foo", "foo") { EnableSsl = true };
            notifier.Notify(new EmailSubcriber(), new SkypeNotifierData());
        }

        [TestMethod]
        [ExpectedException(typeof(AntelopeInvalidParameter))]
        public void SkypeNotifierInvalidData()
        {
            var notifier = new SkypeNotifier();
            notifier.Notify(new EmailSubcriber(), new EmailNotifierData());
        }
        
        [TestMethod]
        [ExpectedException(typeof(AntelopeUnknownTarget))]
        public void SkypeNotifierUnknownTarget()
        {
            var notifier = new SkypeNotifier();
            notifier.AttachToSkype();
            notifier.Notify(
                new SkypeSubcriber() { Handle = "anonymous" },
                new SkypeNotifierData() { Message = "send me" });
        }

        [TestMethod]
        public void SkypeNotifierSuccessful()
        {
            var notifier = new SkypeNotifier();
            notifier.AttachToSkype();
            notifier.Notify(
                new SkypeSubcriber() { Handle = "legoslight" },
                new SkypeNotifierData() { Message = "antelope - message" });
        }
    }
}
