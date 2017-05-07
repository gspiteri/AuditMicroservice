namespace logging.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AuditModel : DbContext
    {
        public AuditModel()
            : base("name=AuditModel")
        {
        }

        public virtual DbSet<Audit> Audits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
