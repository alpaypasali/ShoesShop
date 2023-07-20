﻿using Microsoft.EntityFrameworkCore;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.DataAccess.Concrete.Data
{
    public class ShoeShopDbContext : DbContext
    {

        public ShoeShopDbContext(DbContextOptions<ShoeShopDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Order>Orders { get; set; }     
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentCard> PaymentCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>().HasOne(p => p.Brand)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.BrandID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>().HasOne(p => p.Color)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ColorID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>().HasOne(p => p.Gender)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.GenderID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Stock>().HasOne(p => p.Product)
                .WithMany(c => c.Stocks)
                .HasForeignKey(p => p.ProductID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Favorite>().HasOne(p => p.Product)
                .WithMany(c => c.Favorites)
                .HasForeignKey(p => p.ProductID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Favorite>().HasOne(p => p.User)
                .WithMany(c => c.Favorites)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
