namespace Antelope.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accessibility")]
    public partial class Accessibility
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int IdRole { get; set; }

        [Required]
        [StringLength(2000)]
        public string URL { get; set; }

        public int Priviledge { get; set; }

        [Required]
        [StringLength(150)]
        public string CheckingType { get; set; }
    }
}
