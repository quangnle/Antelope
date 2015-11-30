using Antelope.Notifier.Reports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Tests
{
    [TestClass]
    public class FormatterTests
    {
        [TestMethod]
        public void Formatter_DefaultContent()
        {
            var formatter = new DefaultFormatter();
            var content = formatter.Format("1\n2\n3\n4\n5");
            Assert.AreEqual("1\n2\n3\n4\n5", content);
        }

        [TestMethod]
        public void Formatter_FormatWebContent()
        {
            var formatter = new WebFormatter();
            var content = formatter.Format("1\n2\n3\n4\n5");
            Assert.AreEqual("1<br />2<br />3<br />4<br />5", content);
        }
    }
}
