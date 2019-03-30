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

        Task<OrderItem> UpdateOrderItemQuantity(Guid orderItemId, int quantity, CancellationToken cancellationToken = default);

        Task RemoveOrderItem(Guid orderItem, CancellationToken cancellationToken = default);
    }
}
