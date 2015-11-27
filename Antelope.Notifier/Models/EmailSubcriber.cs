using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Models
{
    public class EmailSubcriber: ISubcriber
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }

        public string Name 
        {
            get { return "Email Subcriber"; } 
        }
    }
}
