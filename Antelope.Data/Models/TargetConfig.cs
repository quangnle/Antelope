namespace Antelope.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TargetConfig")]
    public partial class TargetConfig
    {
        public int Id { get; set; }

        public int IdAccount { get; set; }

        public int IdTarget { get; set; }

        public double MaxLimit { get; set; }
    }
}
