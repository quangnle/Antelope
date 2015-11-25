namespace Antelope.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Action")]
    public partial class Action
    {
        public int Id { get; set; }

        public int IdExceedIncident { get; set; }

        public DateTime ActionTime { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public int IdAccount { get; set; }

        public double Balance { get; set; }

        public virtual ExceedIncident ExceedIncident { get; set; }
    }
}
