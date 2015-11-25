namespace Antelope.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountConfig")]
    public partial class AccountConfig
    {
        public int Id { get; set; }

        public int IdAccount { get; set; }

        public double MinRemain { get; set; }

        public double NotifyThreshold { get; set; }

        public DateTime StartEffectiveDate { get; set; }

        public DateTime EndEffectiveDate { get; set; }

        public double AutoActionThreshold { get; set; }

        public int NumberOfRetries { get; set; }

        public int MonitorPeriod { get; set; }
    }
}
