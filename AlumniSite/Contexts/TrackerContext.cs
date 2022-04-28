using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AlumniSite.Models;

namespace AlumniSite.Contexts
{
    public partial class TrackerContext : DbContext
    {
        public TrackerContext()
        {
        }

        public TrackerContext(DbContextOptions<TrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<AlumniUser> AlumniUsers { get; set; } = null!;
        public virtual DbSet<Employer> Employers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Initial Catalog=AlumniTracker;Data Source=.;trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId)
                    .ValueGeneratedNever()
                    .HasColumnName("AddressID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(50)
                    .HasColumnName("Address");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(10);

                entity.Property(e => e.State).HasMaxLength(2);

                entity.Property(e => e.Zip).HasMaxLength(10);
            });

            modelBuilder.Entity<AlumniUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__AlumniUs__1788CC4CE62D00D5");

                entity.ToTable("AlumniUser");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AdminType).HasMaxLength(150);

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Degree).HasMaxLength(100);

                entity.Property(e => e.EmployerId).HasColumnName("EmployerID");

                entity.Property(e => e.EmployerName).HasMaxLength(250);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.UserPw)
                    .HasMaxLength(50)
                    .HasColumnName("UserPW");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.YearGraduated).HasMaxLength(4);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.AlumniUsers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_AlumniUser_Addresses");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.AlumniUsers)
                    .HasForeignKey(d => d.EmployerId)
                    .HasConstraintName("fk_AlumniUser_Employer");

                entity.HasMany(d => d.Employers)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "EmployerAlumniUser",
                        l => l.HasOne<Employer>().WithMany().HasForeignKey("EmployerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_EmployerAlumniUser_Employer"),
                        r => r.HasOne<AlumniUser>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_EmployerAlumniUser_AlumniUser"),
                        j =>
                        {
                            j.HasKey("UserId", "EmployerId");

                            j.ToTable("EmployerAlumniUser");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");

                            j.IndexerProperty<int>("EmployerId").HasColumnName("EmployerID");
                        });
            });

            modelBuilder.Entity<Employer>(entity =>
            {
                entity.ToTable("Employer");

                entity.Property(e => e.EmployerId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployerID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.EmployerName).HasMaxLength(250);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Employers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Employer_Addresses");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
