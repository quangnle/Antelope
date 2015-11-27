using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Exceptions
{
    public enum SkypeNotifierErrorCode
    {
        AttachError = 1,
    }

    public class AntelopeSkypeNotifierException : Exception
    {
        private SkypeNotifierErrorCode _code;
        public AntelopeSkypeNotifierException(SkypeNotifierErrorCode code)
        {
            _code = code;
        }

        public override string ToString()
        {
            return string.Format("AntelopeSkypeNotifierException: {0}", (int)_code);
        }
    }
}
