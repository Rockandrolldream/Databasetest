using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Databasetest.Models
{
    public partial class BallingdatabaseContext : DbContext
    {
        public BallingdatabaseContext()
        {
        }

        public BallingdatabaseContext(DbContextOptions<BallingdatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cereal> Cereals { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Ballingdatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cereal>(entity =>
            {
                entity.ToTable("cereal");

                entity.Property(e => e.Calories).HasColumnName("calories");

                entity.Property(e => e.Carbo).HasColumnName("carbo");

                entity.Property(e => e.Cups).HasColumnName("cups");

                entity.Property(e => e.Fat).HasColumnName("fat");

                entity.Property(e => e.Fiber).HasColumnName("fiber");

                entity.Property(e => e.Mfr)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("mfr");

                entity.Property(e => e.Name)
                    .HasMaxLength(38)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Picture)
                    .IsUnicode(false)
                    .HasColumnName("picture");

                entity.Property(e => e.Potass).HasColumnName("potass");

                entity.Property(e => e.Protein).HasColumnName("protein");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Shelf).HasColumnName("shelf");

                entity.Property(e => e.Sodium).HasColumnName("sodium");

                entity.Property(e => e.Sugars).HasColumnName("sugars");

                entity.Property(e => e.Type)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.Vitamins).HasColumnName("vitamins");

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
