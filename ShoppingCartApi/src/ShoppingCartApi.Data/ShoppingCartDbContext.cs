using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCartApi.Data.Entities;

namespace ShoppingCartApi.Data
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<OrderItemEntity> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(255);
                entity.Property(e => e.LastName).HasMaxLength(255);
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne(e => e.User)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_User_Orders");
            });

            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(1000);
            });

            modelBuilder.Entity<OrderItemEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne(e => e.Product)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.ProductId)
                    .HasConstraintName("FK_Product_OrderItems");
                entity.HasOne(e => e.Order)
                    .WithMany(e => e.Items)
                    .HasForeignKey(e => e.OrderId)
                    .HasConstraintName("FK_Order_OrderItems");
            });

        }
    }
}
