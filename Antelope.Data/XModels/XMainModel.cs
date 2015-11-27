using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Data.Models
{
    public partial class MainModel
    {
        public void ReloadAllDatabase()
        {
            var context = ((IObjectContextAdapter)this).ObjectContext;
            var refreshableObjects = this.ChangeTracker.Entries().Select(c => c.Entity).ToList();
            context.Refresh(RefreshMode.StoreWins, refreshableObjects);
        }
    }
}
