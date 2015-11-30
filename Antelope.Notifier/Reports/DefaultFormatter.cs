using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Reports
{
    public class DefaultFormatter: IFormatter
    {
        public string Format(string content)
        {
            return content;
        }
    }
}
