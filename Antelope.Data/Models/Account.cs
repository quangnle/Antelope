namespace Antelope.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public Account()
        {
            TargetConfigs = new HashSet<TargetConfig>();
            TargetConfigs1 = new HashSet<TargetConfig>();
        }

        public int Id { get; set; }

        public int IdBank { get; set; }

        public int AccountType { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Number { get; set; }

        public double Balance { get; set; }

        public int Status { get; set; }

        public virtual Bank Bank { get; set; }

        public virtual ICollection<TargetConfig> TargetConfigs { get; set; }

        public virtual ICollection<TargetConfig> TargetConfigs1 { get; set; }
    }
}
