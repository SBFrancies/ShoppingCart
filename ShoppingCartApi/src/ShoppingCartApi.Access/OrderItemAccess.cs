using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShoppingCartApi.Common.Interface;
using ShoppingCartApi.Common.Models;
using ShoppingCartApi.Data;

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
            throw new NotImplementedException();
        }

        public async Task<OrderItem> UpdateOrderItemQuantity(Guid orderItemId, int quantity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveOrderItem(Guid orderItem, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
