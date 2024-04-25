using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CommunityConnections.Models
{
    public partial class localnewsContext : DbContext
    {
        public localnewsContext()
        {
        }

        public localnewsContext(DbContextOptions<localnewsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alert> Alerts { get; set; } = null!;
        public virtual DbSet<BuildVersion> BuildVersions { get; set; } = null!;
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("conn string here");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alert>(entity =>
            {
                entity.ToTable("Alert");

                entity.Property(e => e.AlertId).HasColumnName("AlertID");

                entity.Property(e => e.AlertDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AlertTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AlertType)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TimePosted).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Alerts)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_Alert_Status");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Alerts)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK_Alert_User");
            });

            modelBuilder.Entity<BuildVersion>(entity =>
            {
                entity.HasKey(e => e.SystemInformationId)
                    .HasName("PK__BuildVer__35E58ECAE4AD8073");

                entity.ToTable("BuildVersion");

                entity.Property(e => e.SystemInformationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SystemInformationID");

                entity.Property(e => e.DatabaseVersion)
                    .HasMaxLength(25)
                    .HasColumnName("Database Version");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VersionDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("ErrorLog");

                entity.Property(e => e.ErrorLogId).HasColumnName("ErrorLogID");

                entity.Property(e => e.ErrorMessage).HasMaxLength(4000);

                entity.Property(e => e.ErrorProcedure).HasMaxLength(126);

                entity.Property(e => e.ErrorTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserName).HasMaxLength(128);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.StatusName)
                    .HasName("PK__Status__05E7698BD1E58565");

                entity.ToTable("Status");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__User__C9F28457DF8B7B61");

                entity.ToTable("User");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AuthAnswer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AuthQuestion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.HasSequence<int>("SalesOrderNumber", "SalesLT");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
