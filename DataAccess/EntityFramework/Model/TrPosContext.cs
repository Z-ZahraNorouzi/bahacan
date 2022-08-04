using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data.Entity.SqlServer;

namespace DataAccess.EntityFramework.Model
{
    public partial class TrPosContext : DbContext
    {
        public TrPosContext()
        {
        }

        public TrPosContext(DbContextOptions<TrPosContext> options)
            : base(options)
        {
        }

        #region Security

        public virtual DbSet<Action> Actions { get; set; } = null!;
        public virtual DbSet<LoginHistory> LoginHistories { get; set; } = null!;
        public virtual DbSet<LoginStatus> LoginStatuses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RolePermission> RolePermissions { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServiceAction> ServiceActions { get; set; } = null!;
        public virtual DbSet<ServiceItem> ServiceItems { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;
        public virtual DbSet<UserInRole> UserInRoles { get; set; } = null!;
        public virtual DbSet<UserInfo> UserInfos { get; set; } = null!;
        public virtual DbSet<UserPermission> UserPermissions { get; set; } = null!;
        public virtual DbSet<UserServiceItem> UserServiceItems { get; set; } = null!;
        public virtual DbSet<UserTenant> UserTenants { get; set; } = null!;

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=TrPos;user id=sa;password=123456;TrustServerCertificate=True;",
                                options => options.EnableRetryOnFailure());

            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Security
            modelBuilder.Entity<Action>(entity =>
            {
                entity.ToTable("Action", "Security");

                entity.Property(e => e.ActionId).ValueGeneratedOnAdd();

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<LoginHistory>(entity =>
            {
                entity.ToTable("LoginHistory", "Security");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpireDateTime).HasColumnType("datetime");

                entity.Property(e => e.LoginDateTime).HasColumnType("datetime");

                entity.Property(e => e.LogoutDateTime).HasColumnType("datetime");

                entity.Property(e => e.TenantId).HasDefaultValueSql("((2))");

                entity.Property(e => e.UserHostAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.LoginStatus)
                    .WithMany(p => p.LoginHistories)
                    .HasForeignKey(d => d.LoginStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginHistory_LoginStatus");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.LoginHistories)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginHistory_Tenant");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoginHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginHistory_UserInfo");
            });

            modelBuilder.Entity<LoginStatus>(entity =>
            {
                entity.ToTable("LoginStatus", "Security");

                entity.HasIndex(e => e.TitleFa, "UQ_LoginStatus_TitleFa")
                    .IsUnique();

                entity.Property(e => e.TitleEn)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TitleFa).HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "Security");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("RolePermission", "Security");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenantId).HasDefaultValueSql("((2))");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermission_Roles");

                entity.HasOne(d => d.ServiceAction)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.ServiceActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermission_ServiceAction");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermission_Tenant");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service", "Security");

                entity.Property(e => e.ServiceId).ValueGeneratedNever();

                entity.Property(e => e.Controller).HasMaxLength(200);

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Icon).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Service_Service");
            });

            modelBuilder.Entity<ServiceAction>(entity =>
            {
                entity.ToTable("ServiceAction", "Security");

                entity.Property(e => e.ApiController).HasMaxLength(256);

                entity.Property(e => e.ApiService).HasMaxLength(256);

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.ServiceActions)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceAction_Action");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceActions)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceAction_Service");
            });

            modelBuilder.Entity<ServiceItem>(entity =>
            {
                entity.ToTable("ServiceItem", "Security");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenantId).HasDefaultValueSql("((2))");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceItems)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceItem_Service");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.ServiceItems)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceItem_Tenant");
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("Tenant", "Security");

                entity.Property(e => e.Title).HasMaxLength(500);
            });

            modelBuilder.Entity<UserInRole>(entity =>
            {
                entity.ToTable("UserInRole", "Security");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenantId).HasDefaultValueSql("((2))");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserInRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserInRole_Roles");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.UserInRoles)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserInRole_Tenant");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserInRole_UserInfo");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.ToTable("UserInfo", "Security");

                entity.Property(e => e.ActivationCode).HasMaxLength(6);

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.MobileNo).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.ToTable("UserPermission", "Security");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenantId).HasDefaultValueSql("((2))");

                entity.HasOne(d => d.ServiceAction)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(d => d.ServiceActionId)
                    .HasConstraintName("FK_UserPermission_ServiceAction");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPermission_Tenant");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPermission_UserInfo");
            });

            modelBuilder.Entity<UserServiceItem>(entity =>
            {
                entity.ToTable("UserServiceItem", "Security");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ServiceItem)
                    .WithMany(p => p.UserServiceItems)
                    .HasForeignKey(d => d.ServiceItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserServiceItem_ServiceItem");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserServiceItems)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserServiceItem_UserInfo");
            });

            modelBuilder.Entity<UserTenant>(entity =>
            {
                entity.ToTable("UserTenant", "Security");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.UserTenants)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_UserTenant_Tenant");

                entity.HasOne(d => d.UserInfo)
                    .WithMany(p => p.UserTenants)
                    .HasForeignKey(d => d.UserInfoId)
                    .HasConstraintName("FK_UserTenant_UserInfo");
            });

            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
