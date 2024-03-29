﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingCartApi.Data;

namespace ShoppingCartApi.Data.Migrations
{
    [DbContext(typeof(ShoppingCartDbContext))]
    [Migration("20190330224332_ConcurrencyToken")]
    partial class ConcurrencyToken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShoppingCartApi.Data.Entities.CustomerEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("780d165e-fe91-4162-a90e-dbfe5e9a2b35"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 178, DateTimeKind.Unspecified).AddTicks(4624), new TimeSpan(0, 0, 0, 0, 0)),
                            FirstName = "John",
                            LastName = "Smith"
                        },
                        new
                        {
                            Id = new Guid("56def7d9-061c-4a26-bdf6-81695b074dd7"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 178, DateTimeKind.Unspecified).AddTicks(5387), new TimeSpan(0, 0, 0, 0, 0)),
                            FirstName = "Jane",
                            LastName = "Brown"
                        },
                        new
                        {
                            Id = new Guid("de268b5f-0074-4c8c-a765-a2eecaf0826b"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 178, DateTimeKind.Unspecified).AddTicks(5392), new TimeSpan(0, 0, 0, 0, 0)),
                            FirstName = "Kevin",
                            LastName = "Ali"
                        });
                });

            modelBuilder.Entity("ShoppingCartApi.Data.Entities.OrderEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<Guid>("CustomerId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1f64e20c-7d3e-482b-b3d7-b2f72a007d52"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 193, DateTimeKind.Unspecified).AddTicks(4268), new TimeSpan(0, 0, 0, 0, 0)),
                            CustomerId = new Guid("780d165e-fe91-4162-a90e-dbfe5e9a2b35")
                        },
                        new
                        {
                            Id = new Guid("14ee82e7-3d8f-422c-9d6e-416710564141"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 193, DateTimeKind.Unspecified).AddTicks(4915), new TimeSpan(0, 0, 0, 0, 0)),
                            CustomerId = new Guid("780d165e-fe91-4162-a90e-dbfe5e9a2b35")
                        },
                        new
                        {
                            Id = new Guid("1ba79335-00a9-49d2-bee4-d603aa10aaf6"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 193, DateTimeKind.Unspecified).AddTicks(4925), new TimeSpan(0, 0, 0, 0, 0)),
                            CustomerId = new Guid("56def7d9-061c-4a26-bdf6-81695b074dd7")
                        },
                        new
                        {
                            Id = new Guid("4b61ecab-117a-4cbd-aff0-b56573c82083"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 193, DateTimeKind.Unspecified).AddTicks(4925), new TimeSpan(0, 0, 0, 0, 0)),
                            CustomerId = new Guid("de268b5f-0074-4c8c-a765-a2eecaf0826b")
                        },
                        new
                        {
                            Id = new Guid("51b5cff8-b2ce-4de8-ab8a-16b6343e105a"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 193, DateTimeKind.Unspecified).AddTicks(4939), new TimeSpan(0, 0, 0, 0, 0)),
                            CustomerId = new Guid("de268b5f-0074-4c8c-a765-a2eecaf0826b")
                        });
                });

            modelBuilder.Entity("ShoppingCartApi.Data.Entities.OrderItemEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<Guid>("OrderId");

                    b.Property<Guid>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId", "OrderId")
                        .IsUnique();

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a19c1898-4ce6-4a45-b4f0-3bb1436c2fed"),
                            ConcurrencyToken = "b0f64002-9f71-4b23-bd9e-2cd85118bd84",
                            OrderId = new Guid("1f64e20c-7d3e-482b-b3d7-b2f72a007d52"),
                            ProductId = new Guid("c31a24e6-54e7-4644-b62e-c587001ed18f"),
                            Quantity = 1
                        },
                        new
                        {
                            Id = new Guid("f101e417-8f49-4373-a9a3-daf6eac7d5c9"),
                            ConcurrencyToken = "01f51068-95fb-4a2a-8867-5734827da392",
                            OrderId = new Guid("1f64e20c-7d3e-482b-b3d7-b2f72a007d52"),
                            ProductId = new Guid("b8c7a7fa-3c84-4a4a-bd9f-36bca1a9ad13"),
                            Quantity = 22
                        },
                        new
                        {
                            Id = new Guid("b34f7d37-ced0-4880-8d75-b0836cd5924f"),
                            ConcurrencyToken = "74e64c8a-414a-44f0-a0e5-a56761f38025",
                            OrderId = new Guid("14ee82e7-3d8f-422c-9d6e-416710564141"),
                            ProductId = new Guid("03fba56c-41ee-48cf-b297-26a9a8675151"),
                            Quantity = 4
                        },
                        new
                        {
                            Id = new Guid("bb774e9d-cb0a-41fe-99bd-97780ca49217"),
                            ConcurrencyToken = "2b588d55-c6bd-4749-b83c-4d9fc1f510e0",
                            OrderId = new Guid("14ee82e7-3d8f-422c-9d6e-416710564141"),
                            ProductId = new Guid("b8c7a7fa-3c84-4a4a-bd9f-36bca1a9ad13"),
                            Quantity = 3
                        },
                        new
                        {
                            Id = new Guid("f6c2afd9-15e4-4e5b-beda-8844e26a402e"),
                            ConcurrencyToken = "24a16788-81ff-4bcb-bdff-372fb1d25b0b",
                            OrderId = new Guid("1ba79335-00a9-49d2-bee4-d603aa10aaf6"),
                            ProductId = new Guid("6407d802-10e5-47c6-9997-69c7bbacdc75"),
                            Quantity = 17
                        },
                        new
                        {
                            Id = new Guid("43e4ba9f-5d82-4414-9702-5ca3def48168"),
                            ConcurrencyToken = "7ce8ee55-6442-49af-8156-bd6f507c1bab",
                            OrderId = new Guid("4b61ecab-117a-4cbd-aff0-b56573c82083"),
                            ProductId = new Guid("03fba56c-41ee-48cf-b297-26a9a8675151"),
                            Quantity = 8
                        },
                        new
                        {
                            Id = new Guid("d54fe794-4448-47ae-9286-31644d6a353d"),
                            ConcurrencyToken = "7bde999e-9048-4061-bec2-2ded70768c35",
                            OrderId = new Guid("51b5cff8-b2ce-4de8-ab8a-16b6343e105a"),
                            ProductId = new Guid("03fba56c-41ee-48cf-b297-26a9a8675151"),
                            Quantity = 1
                        },
                        new
                        {
                            Id = new Guid("10f1ac9e-65ed-44c1-94d5-e76d3c532889"),
                            ConcurrencyToken = "c65ec5b7-87be-4dd3-9150-2ac64f65c1a7",
                            OrderId = new Guid("51b5cff8-b2ce-4de8-ab8a-16b6343e105a"),
                            ProductId = new Guid("b8c7a7fa-3c84-4a4a-bd9f-36bca1a9ad13"),
                            Quantity = 1
                        },
                        new
                        {
                            Id = new Guid("29e4485c-483b-4ae7-8643-97a5d0d8a784"),
                            ConcurrencyToken = "21bb1e01-4652-421b-afe5-7081e4cbc9c7",
                            OrderId = new Guid("51b5cff8-b2ce-4de8-ab8a-16b6343e105a"),
                            ProductId = new Guid("6407d802-10e5-47c6-9997-69c7bbacdc75"),
                            Quantity = 2
                        });
                });

            modelBuilder.Entity("ShoppingCartApi.Data.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c31a24e6-54e7-4644-b62e-c587001ed18f"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 195, DateTimeKind.Unspecified).AddTicks(5633), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Shoes"
                        },
                        new
                        {
                            Id = new Guid("03fba56c-41ee-48cf-b297-26a9a8675151"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 195, DateTimeKind.Unspecified).AddTicks(6298), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Wardrobe"
                        },
                        new
                        {
                            Id = new Guid("b8c7a7fa-3c84-4a4a-bd9f-36bca1a9ad13"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 195, DateTimeKind.Unspecified).AddTicks(6308), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Coffee Cup"
                        },
                        new
                        {
                            Id = new Guid("6407d802-10e5-47c6-9997-69c7bbacdc75"),
                            CreatedAt = new DateTimeOffset(new DateTime(2019, 3, 30, 22, 43, 31, 195, DateTimeKind.Unspecified).AddTicks(6308), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Curtains"
                        });
                });

            modelBuilder.Entity("ShoppingCartApi.Data.Entities.OrderEntity", b =>
                {
                    b.HasOne("ShoppingCartApi.Data.Entities.CustomerEntity", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Customer_Orders")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShoppingCartApi.Data.Entities.OrderItemEntity", b =>
                {
                    b.HasOne("ShoppingCartApi.Data.Entities.OrderEntity", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_Order_OrderItems")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShoppingCartApi.Data.Entities.ProductEntity", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Product_OrderItems")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
