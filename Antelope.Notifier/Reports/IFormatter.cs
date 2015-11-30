using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Reports
{
    public interface IFormatter
    {
        string Format(string content);
    }
}
