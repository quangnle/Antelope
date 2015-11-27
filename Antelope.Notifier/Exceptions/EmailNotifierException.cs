using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Exceptions
{
    public class AntelopeInvalidEmailFormat : Exception { }
    public class AntelopeSmtpException : Exception
    {
        private string _message;
        public AntelopeSmtpException(string message)
        {
            _message = message;
        }
        public override string ToString()
        {
            return string.Format("AntelopeSmtpException: {0}", _message);
        }
    }
}
