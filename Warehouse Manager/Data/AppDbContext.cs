using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Warehouse_Manager.MVVM.Model;


namespace Warehouse_Manager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sc => sc.Product)
                .WithMany()
                .HasForeignKey(sc => sc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
    }
}
