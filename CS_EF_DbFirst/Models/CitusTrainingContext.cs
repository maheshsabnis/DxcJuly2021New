using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CS_EF_DbFirst.Models
{
    public partial class CitusTrainingContext : DbContext
    {
        public CitusTrainingContext()
        {
        }

        public CitusTrainingContext(DbContextOptions<CitusTrainingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CitusTraining;Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryRowId);

                entity.HasIndex(e => e.CategoryId, "IX_Categories_CategoryId")
                    .IsUnique();

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SubCategoryName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductRowId);

                entity.HasIndex(e => e.CategoryRowId, "IX_Products_CategoryRowId");

                entity.HasIndex(e => e.ProductId, "IX_Products_ProductId")
                    .IsUnique();

                entity.Property(e => e.Desicription)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.ProductId).IsRequired();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.CategoryRow)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryRowId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
