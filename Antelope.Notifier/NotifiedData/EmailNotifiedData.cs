using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.NotifiedData
{
    public class EmailNotifiedData: BaseNotifiedData
    {
        public string Email { get; set; }
        public string DisplayedName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
