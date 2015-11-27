using Antelope.Notifier;
using Antelope.Notifier.Exceptions;
using Antelope.Notifier.Models;
using Antelope.Notifier.Notifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Tests
{
    [TestClass]
    class ObserverTests
    {
        [TestMethod]
        public void Observer_NotifierShouldNotifyToSubcribers()
        {
            var mockNotifier = new Mock<INotifier>();
            var fooNotifier = new Mock<INotifier>();
            var voidNotifier = new Mock<INotifier>();

            var mockSubcriber = new Mock<ISubcriber>();
            mockSubcriber.Setup(s => s.Name).Returns("Kong Subcriber");

            var fooSubcriber = new Mock<ISubcriber>();
            fooSubcriber.Setup(s => s.Name).Returns("Foo Subcriber");

            var mockData = new Mock<BaseNotifierData>();

            var observer = new AntelopeObserver();
            observer.AddNotifier(1, mockNotifier.Object);
            observer.AddNotifier(2, fooNotifier.Object);
            observer.AddNotifier(3, voidNotifier.Object);

            observer.Register(1, mockSubcriber.Object);
            observer.Register(2, fooSubcriber.Object);

            observer.NotifyAll(mockData.Object);
            observer.Notify(1, mockData.Object);

            mockNotifier.Verify(n => n.Notify(It.Is<ISubcriber>(s => s.Name == "Kong Subcriber"), It.IsAny<BaseNotifierData>()));
            mockNotifier.Verify(n => n.Notify(It.IsAny<ISubcriber>(), It.IsAny<BaseNotifierData>()), Times.Exactly(2));

            fooNotifier.Verify(n => n.Notify(It.Is<ISubcriber>(s => s.Name == "Foo Subcriber"), It.IsAny<BaseNotifierData>()));
            fooNotifier.Verify(n => n.Notify(It.IsAny<ISubcriber>(), It.IsAny<BaseNotifierData>()), Times.Exactly(1));

            voidNotifier.Verify(n => n.Notify(It.IsAny<ISubcriber>(), It.IsAny<BaseNotifierData>()), Times.Exactly(0));
        }

        [TestMethod]
        [ExpectedException(typeof(AntelopeNotiferNotFound))]
        public void Observer_AddSubcriberToInvalidChannel()
        {
            var mockSubcriber = new Mock<ISubcriber>();

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
    }
}
