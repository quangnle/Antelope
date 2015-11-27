using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Models
{
    public class EmailSubcriber: BaseSubcriber
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }

        public override string Name()
        {
            return "Email Subcriber";
        }
    }
}
