using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShoppingCartApi.Common.Models;

namespace ShoppingCartApi.Common.Interface
{
    public interface IOrderItemAccess
    {
        OrderItem GetOrderItem(Guid orderItemId);

        Task<OrderItem> CreateOrderItemAsync(Guid orderId, Guid productId, int quantity, CancellationToken cancellationToken = default);

        Task<OrderItem> UpdateOrderItemQuantityAsync(Guid orderItemId, int quantity, CancellationToken cancellationToken = default);

        Task RemoveOrderItemAsync(Guid orderItemId, CancellationToken cancellationToken = default);
    }
}
