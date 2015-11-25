namespace Antelope.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExceedIncident")]
    public partial class ExceedIncident
    {
        public ExceedIncident()
        {
            Actions = new HashSet<Action>();
        }

        public int Id { get; set; }

        public DateTime DetectedAt { get; set; }

        public int IdAccount { get; set; }

        public virtual ICollection<Action> Actions { get; set; }
    }
}
