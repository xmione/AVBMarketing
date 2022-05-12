using AVBMarketing.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace AVBMarketing.EF
{
    public partial class AVBMarketingContext : DbContext
    {
        public AVBMarketingContext()
        {
        }

        public AVBMarketingContext(DbContextOptions<AVBMarketingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Meeting> Meetings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=AVBMarketing;User Id=sa;Password=P@ssw0rd123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
