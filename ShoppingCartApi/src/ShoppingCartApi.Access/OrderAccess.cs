using ShoppingCartApi.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCartApi.Common.Models;
using ShoppingCartApi.Data;
using ShoppingCartApi.Data.Entities;

namespace ShoppingCartApi.Access
{
    public class OrderAccess : IOrderAccess
    {
        private readonly Func<ShoppingCartDbContext> _dbContextFactory;

        public OrderAccess(Func<ShoppingCartDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IReadOnlyCollection<Order> GetAllOrders()
        {
            using (ShoppingCartDbContext context = _dbContextFactory())
            {
                return context.Orders
                    .Include(a => a.Items)
                    .ThenInclude(a => a.Product)
                    .Select(a => BuildOrderFromEntity(a))
                    .ToList();
            }
        }

        public IReadOnlyCollection<Order> GetUserOrders(Guid userId)
        {
            using (ShoppingCartDbContext context = _dbContextFactory())
            {
                return context.Orders
                    .Include(a => a.Items)
                    .ThenInclude(a => a.Product)
                    .Where(a => a.UserId == userId)
                    .Select(a => BuildOrderFromEntity(a))
                    .ToList();
            }
        }

        public Order GetOrder(Guid orderId)
        {
            using (ShoppingCartDbContext context = _dbContextFactory())
            {
                return BuildOrderFromEntity(context.Orders
                    .Include(a => a.Items)
                    .ThenInclude(a => a.Product)
                    .SingleOrDefault(a => a.Id == orderId));

            }
        }

        public async Task<Order> CreateOrder(Guid userId, CancellationToken cancellationToken = default)
        {
            using (ShoppingCartDbContext context = _dbContextFactory())
            {
                OrderEntity entity = new OrderEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTimeOffset.UtcNow,
                    UserId = userId,
                };

                context.Orders.Add(entity);

                await context.SaveChangesAsync(cancellationToken);

                return BuildOrderFromEntity(entity);
            }
        }

        public async Task ClearOrder(Guid orderId, CancellationToken cancellationToken = default)
        {
            using (ShoppingCartDbContext context = _dbContextFactory())
            {
                IEnumerable<OrderItemEntity> orderItems = context.OrderItems.Where(a => a.OrderId == orderId);

                foreach (OrderItemEntity item in orderItems)
                {
                    context.OrderItems.Remove(item);
                }

                await context.SaveChangesAsync(cancellationToken);
            }
        }

        private Order BuildOrderFromEntity(OrderEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new Order
            {
                Id = entity.Id,
                Items = entity.Items?.Select(a => new OrderItem
                    {
                        Quantity = a.Quantity,
                        Product = new Product
                        {
                            Name = a.Product?.Name,
                            Description = a.Product?.Description,
                        },
                    }
                ).ToList(),
            };
        }
    }
}
