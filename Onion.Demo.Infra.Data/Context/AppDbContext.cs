using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Onion.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Infra.Data.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<Order>(order =>
            {
                order.ToTable("Orders");
                order.HasKey(o => o.Id);

                order.Property(o => o.TotalAmount)
                     .HasColumnType("decimal(18,2)");

                order.Property(o => o.CreatedAt)
                     .IsRequired();

                order.OwnsMany(o => o.Items, item =>
                {
                    item.ToTable("OrderItems");

                    item.WithOwner().HasForeignKey("OrderId"); // 外键

                    item.Property<Guid>("Id"); // 主键（shadow）
                    item.HasKey("Id");
                    item.Property(i => i.ProductId).IsRequired();
                    item.Property(i => i.Quantity).IsRequired();
                    item.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");
                });

            });


        }
    }
}
