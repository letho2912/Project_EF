using Microsoft.EntityFrameworkCore;
using Project_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Data
{
    public class Connect : DbContext
    {
        public Connect(DbContextOptions<Connect> options) : base(options)
        {

        }
        public DbSet<ParentCate> ParentCate { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }     

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<OrderDetail>().HasKey(sc => new {
                    sc.OrderId,
                    sc.ProductId
                });

                modelBuilder.Entity<OrderDetail>()
                    .HasOne<Order>(sc => sc.Order)
                    .WithMany(s => s.OrderDetail)
                    .HasForeignKey(sc => sc.OrderId);


                modelBuilder.Entity<OrderDetail>()
                .HasOne<Product>(sc => sc.Product)
                .WithMany(s => s.OrderDetail)
                .HasForeignKey(sc => sc.ProductId);

            }
        }
      }