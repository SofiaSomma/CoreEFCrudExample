using CoreEFCrud.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEFCrud.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                        .HasOne(a => a.Customer)
                        .WithMany(b => b.Orders)
                        .HasForeignKey(b => b.CustomerId)
                        .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<OrderProduct>()
                            .HasKey(s => new { s.OrderId, s.ProductId });

            modelBuilder.Entity<OrderProduct>()
                          .HasOne(a => a.Order)
                          .WithMany(b => b.OrderProducts)
                          .HasForeignKey(bc => bc.OrderId)
                          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderProduct>()
                          .HasOne(a => a.Product)
                          .WithMany(b => b.OrderProducts)
                          .HasForeignKey(bc => bc.ProductId)
                          .OnDelete(DeleteBehavior.NoAction);

         


            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, Name = "John", Surname = "Doe" },
                new Customer { CustomerId = 2, Name = "Mickeal", Surname = "Johnson" },
                new Customer { CustomerId = 3, Name = "Christian", Surname = "Tyle" }
                );

            modelBuilder.Entity<Product>().HasData(
               new Product { ProductId = 1, Name = "Pizza" },
               new Product { ProductId = 2, Name = "Spaghetti" },
               new Product { ProductId = 3, Name = "Burger" }
               );


        }
    }
}
