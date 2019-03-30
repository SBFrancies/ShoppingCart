using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCartApi.Common.Exceptions;
using ShoppingCartApi.Common.Interface;
using ShoppingCartApi.Common.Models;
using ShoppingCartApi.Data;
using ShoppingCartApi.Data.Entities;

namespace ShoppingCartApi.Access
{
    public class OrderItemAccess : IOrderItemAccess
    {
        private readonly Func<ShoppingCartDbContext> _dbContextFactory;

        public OrderItemAccess(Func<ShoppingCartDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public OrderItem GetOrderItem(Guid orderItemId)
        {
            using (ShoppingCartDbContext context = _dbContextFactory())
            {
                return BuildOrderItemFromEntity(context.OrderItems.Include(a => a.Product).SingleOrDefault(a => a.Id == orderItemId));
            }
        }

        public async Task<OrderItem> CreateOrderItemAsync(Guid orderId, Guid productId, int quantity, CancellationToken cancellationToken = default)
        {
            using (ShoppingCartDbContext context = _dbContextFactory())
            {
                OrderEntity order = context.Orders.SingleOrDefault(a => a.Id == orderId);

                if (order == null)
                {
                    throw new OrderNotFoundException($"Order with ID {orderId} not found");
                }

                ProductEntity product = context.Products.SingleOrDefault(a => a.Id == productId);

                if (product == null)
                {
                    throw new ProductNotFoundException($"Product with ID {productId} not found");
                }

                OrderItemEntity orderItem = new OrderItemEntity
                {
                    Id = Guid.NewGuid(),
                    Order = order,
                    Product = product,
                    Quantity = quantity,
                };

                context.OrderItems.Add(orderItem);

                await context.SaveChangesAsync(cancellationToken);

                return BuildOrderItemFromEntity(orderItem);
            }
        }

        public async Task<OrderItem> UpdateOrderItemQuantityAsync(Guid orderItemId, int quantity, CancellationToken cancellationToken = default)
        {
            using (ShoppingCartDbContext context = _dbContextFactory())
            {
                OrderItemEntity orderItem = context.OrderItems.Include(a => a.Product)
                    .SingleOrDefault(a => a.Id == orderItemId);

                if (orderItem == null)
                {
                    throw new OrderItemNotFoundException($"OrderItem with ID {orderItemId} not found");
                }

                orderItem.Quantity = quantity;

                await context.SaveChangesAsync(cancellationToken);

                return BuildOrderItemFromEntity(orderItem);
            }
        }

        public async Task RemoveOrderItemAsync(Guid orderItemId, CancellationToken cancellationToken = default)
        {
            using (ShoppingCartDbContext context = _dbContextFactory())
            {
                OrderItemEntity orderItem = context.OrderItems.SingleOrDefault(a => a.Id == orderItemId);

                if (orderItem == null)
                {
                    throw new OrderItemNotFoundException($"OrderItem with ID {orderItemId} not found");
                }

                context.Remove(orderItem);

                await context.SaveChangesAsync(cancellationToken);
            }
        }

        private OrderItem BuildOrderItemFromEntity(OrderItemEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new OrderItem
            {
                Quantity = entity.Quantity,
                Product = new Product
                {
                    Description = entity.Product?.Description,
                    Name = entity.Product?.Name,
                }
            };
        }
    }
}
