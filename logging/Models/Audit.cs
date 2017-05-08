namespace logging.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public interface IAudit
    {
        Guid LogId { get; set; }
        Guid? ParentLogId { get; set; }
        string Message { get; set; }
    }

    [Table("Audit")]
    public class Audit : IAudit
    {
        [Key]
        public Guid LogId { get; set; }

        public Guid? ParentLogId { get; set; }

        [Column(TypeName = "ntext")]
        public string Message { get; set; }
    }
}
