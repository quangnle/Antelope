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
        [TestMethod]
        public void Observer_NotifierShouldNotifyToSubcribers()
        {
            var mockNotifier = new Mock<INotifier>();
            var fooNotifier = new Mock<INotifier>();
            var voidNotifier = new Mock<INotifier>();    

            var mockSubcriber = new Mock<BaseSubcriber>();
            mockSubcriber.Setup(s => s.Name()).Returns("Kong Subcriber");

            var fooSubcriber = new Mock<BaseSubcriber>();
            fooSubcriber.Setup(s => s.Name()).Returns("Foo Subcriber");            

            var mockData = new Mock<BaseNotifierData>();

            var observer = new AntelopeObserver();
            observer.AddNotifier(1, mockNotifier.Object);
            observer.AddNotifier(2, fooNotifier.Object);
            observer.AddNotifier(3, voidNotifier.Object);

            observer.Register(1, mockSubcriber.Object);
            observer.Register(2, fooSubcriber.Object);

            observer.NotifyAll(mockData.Object);
            observer.Notify(1, mockData.Object);

            mockNotifier.Verify(n => n.Notify(It.Is<BaseSubcriber>(s => s.Name() == "Kong Subcriber"), It.IsAny<BaseNotifierData>()));
            mockNotifier.Verify(n => n.Notify(It.IsAny<BaseSubcriber>(), It.IsAny<BaseNotifierData>()), Times.Exactly(2));

            fooNotifier.Verify(n => n.Notify(It.Is<BaseSubcriber>(s => s.Name() == "Foo Subcriber"), It.IsAny<BaseNotifierData>()));
            fooNotifier.Verify(n => n.Notify(It.IsAny<BaseSubcriber>(), It.IsAny<BaseNotifierData>()), Times.Exactly(1));

            voidNotifier.Verify(n => n.Notify(It.IsAny<BaseSubcriber>(), It.IsAny<BaseNotifierData>()), Times.Exactly(0));
        }

        [TestMethod]
        [ExpectedException(typeof(AntelopeNotiferNotFound))]
        public void Observer_AddSubcriberToInvalidChannel()
        {
            var mockSubcriber = new Mock<BaseSubcriber>();

            var observer = new AntelopeObserver();
            observer.Register(1, mockSubcriber.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(AntelopeInvalidNotifier))]
        public void Observer_AddTaintedNotifier()
        {
            var observer = new AntelopeObserver();
            observer.AddNotifier(1, null);
        }

        [TestMethod]
        public void Notifier_TestGmailNotifier()
        {
            var notifier = GmailNotifier.CreateNotifier("peterpan.hx@gmail.com", "Since!990", "Nguyen Tan Cong");
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
    }
}
