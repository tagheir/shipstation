using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Generics.Models
{
    public partial class ShipstationContext : DbContext
    {
        public ShipstationContext()
        {
        }

        public ShipstationContext(DbContextOptions<ShipstationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<EmailAccounts> EmailAccounts { get; set; }
        public virtual DbSet<EmailSender> EmailSender { get; set; }
        public virtual DbSet<OrderDelayMessage> OrderDelayMessage { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<ProductStatus> ProductStatus { get; set; }
        public virtual DbSet<SamsAccount> SamsAccount { get; set; }
        public virtual DbSet<SiteMetas> SiteMetas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-7I6F2DF\\SQLEXPRESS;Database=Shipstation;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<EmailAccounts>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.PrivateKey)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceAccount)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmailSender>(entity =>
            {
                entity.Property(e => e.EmailPurpose)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.EmailStatus)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderDelayMessage>(entity =>
            {
                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SentAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.Property(e => e.ItemError)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ItemFetchStatus1).IsUnicode(false);

                entity.Property(e => e.ItemFetchStatus2).IsUnicode(false);

                entity.Property(e => e.ItemFetchStatus3).IsUnicode(false);

                entity.Property(e => e.ItemOrderedOn).HasColumnType("datetime");

                entity.Property(e => e.ItemOrderedUrl).IsUnicode(false);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnModified).HasColumnType("datetime");

                entity.Property(e => e.OrderCreatedInformationJson)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderCreatedOn).HasColumnType("datetime");

                entity.Property(e => e.OrderCreatedUrl)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderJson)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.OrderWebsite)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Step1Error).IsUnicode(false);

                entity.Property(e => e.Step1Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Step2Error).IsUnicode(false);

                entity.Property(e => e.Step2Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Step3Error).IsUnicode(false);

                entity.Property(e => e.Step3Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductStatus>(entity =>
            {
                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ItemSku)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            });

            modelBuilder.Entity<SamsAccount>(entity =>
            {
                entity.Property(e => e.Cookie).IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Token).IsUnicode(false);
            });

            modelBuilder.Entity<SiteMetas>(entity =>
            {
                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasColumnName("KEY")
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("VALUE")
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
