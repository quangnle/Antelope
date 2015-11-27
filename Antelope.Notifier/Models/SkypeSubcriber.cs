using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Models
{
    public class SkypeSubcriber: ISubcriber
    {
        public string Handle { get; set; }

        public string Name
        {
            get { return "Skype Subcriber"; }
        }
    }
}
