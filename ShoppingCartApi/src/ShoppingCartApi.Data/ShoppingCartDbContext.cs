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

        public DbSet<CustomerEntity> Customers { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<OrderItemEntity> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(255);
                entity.Property(e => e.LastName).HasMaxLength(255);
                entity.HasData(CustomerData());
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne(e => e.Customer)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.CustomerId)
                    .HasConstraintName("FK_Customer_Orders");
                entity.HasData(OrderData());
            });

            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.HasData(ProductData());
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
                entity.HasIndex(e => new {e.ProductId, e.OrderId}).IsUnique();
                entity.Property(e => e.ConcurrencyToken).IsConcurrencyToken();
                entity.HasData(OrderItemData());
            });

        }

        private readonly Guid[] _customerIds = {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()};
        private readonly Guid[] _orderIds = { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()};
        private readonly Guid[] _productIds = {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()};

        private CustomerEntity[] CustomerData()
        {
            return new []
            {
                new CustomerEntity{Id = _customerIds[0], FirstName = "John", LastName = "Smith", CreatedAt = DateTimeOffset.UtcNow},
                new CustomerEntity{Id = _customerIds[1], FirstName = "Jane", LastName = "Brown", CreatedAt = DateTimeOffset.UtcNow},
                new CustomerEntity{Id = _customerIds[2], FirstName = "Kevin", LastName = "Ali", CreatedAt = DateTimeOffset.UtcNow},
            };
        }

        private OrderEntity[] OrderData()
        {
            return new []
            {
                new OrderEntity{Id = _orderIds[0], CustomerId = _customerIds[0], CreatedAt = DateTimeOffset.UtcNow},
                new OrderEntity{Id = _orderIds[1], CustomerId = _customerIds[0], CreatedAt = DateTimeOffset.UtcNow},
                new OrderEntity{Id = _orderIds[2], CustomerId = _customerIds[1], CreatedAt = DateTimeOffset.UtcNow},
                new OrderEntity{Id = _orderIds[3], CustomerId = _customerIds[2], CreatedAt = DateTimeOffset.UtcNow},
                new OrderEntity{Id = _orderIds[4], CustomerId = _customerIds[2], CreatedAt = DateTimeOffset.UtcNow},
            };
        }

        private ProductEntity[] ProductData()
        {
            return new []
            {
                new ProductEntity{Id= _productIds[0], Name = "Shoes", CreatedAt = DateTimeOffset.UtcNow},
                new ProductEntity{Id= _productIds[1], Name = "Wardrobe", CreatedAt = DateTimeOffset.UtcNow},
                new ProductEntity{Id= _productIds[2], Name = "Coffee Cup", CreatedAt = DateTimeOffset.UtcNow},
                new ProductEntity{Id= _productIds[3], Name = "Curtains", CreatedAt = DateTimeOffset.UtcNow},
            };
        }

        private OrderItemEntity[] OrderItemData()
        {
            return new []
            {
                new OrderItemEntity{Id=Guid.NewGuid(), OrderId = _orderIds[0], ProductId = _productIds[0], Quantity = 1},
                new OrderItemEntity{Id=Guid.NewGuid(), OrderId = _orderIds[0], ProductId = _productIds[2], Quantity = 22},
                new OrderItemEntity{Id=Guid.NewGuid(), OrderId = _orderIds[1], ProductId = _productIds[1], Quantity = 4},
                new OrderItemEntity{Id=Guid.NewGuid(), OrderId = _orderIds[1], ProductId = _productIds[2], Quantity = 3},
                new OrderItemEntity{Id=Guid.NewGuid(), OrderId = _orderIds[2], ProductId = _productIds[3], Quantity = 17},
                new OrderItemEntity{Id=Guid.NewGuid(), OrderId = _orderIds[3], ProductId = _productIds[1], Quantity = 8},
                new OrderItemEntity{Id=Guid.NewGuid(), OrderId = _orderIds[4], ProductId = _productIds[1], Quantity = 1},
                new OrderItemEntity{Id=Guid.NewGuid(), OrderId = _orderIds[4], ProductId = _productIds[2], Quantity = 1},
                new OrderItemEntity{Id=Guid.NewGuid(), OrderId = _orderIds[4], ProductId = _productIds[3], Quantity = 2},
            };
        }
    }
}
