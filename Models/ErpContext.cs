using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Transactions;

namespace ErpApi.Models
{
    public partial class ErpContext : DbContext
    {
        public ErpContext()
        {
        }

        public ErpContext(DbContextOptions<ErpContext> options) : base(options)
        {
        }

        public virtual DbSet<Organisation> organisation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organisation>(entity =>
            {
                entity.ToTable("Organisation");  // Table name

                entity.HasKey(e => e.Id)         // Primary Key
                      .HasName("PK_Organisation_Id"); // PK Name
            });

            OnModelCreatingPartial(modelBuilder);
        }



        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
