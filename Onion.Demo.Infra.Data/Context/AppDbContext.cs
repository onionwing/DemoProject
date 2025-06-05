using Microsoft.EntityFrameworkCore;
using Onion.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

                    item.Property<string>("Id"); // 主键（shadow）
                    item.HasKey("Id");
                    item.Property(i => i.ProductId).IsRequired();
                    item.Property(i => i.Quantity).IsRequired();
                    item.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");
                });

            });


        }
    }
}
