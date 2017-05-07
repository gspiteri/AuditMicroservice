namespace logging.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Audit")]
    public partial class Audit
    {
        [Key]
        public Guid LogId { get; set; }

        public Guid? ParentLogId { get; set; }

        [Column(TypeName = "ntext")]
        public string Message { get; set; }
    }
}
