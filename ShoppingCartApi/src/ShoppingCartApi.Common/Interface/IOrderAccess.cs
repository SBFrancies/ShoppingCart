using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShoppingCartApi.Common.Models;

namespace ShoppingCartApi.Common.Interface
{
    public interface IOrderAccess
    {
        IReadOnlyCollection<Order> GetAllOrders();

        IReadOnlyCollection<Order> GetUserOrders(Guid userId);

        Order GetOrder(Guid orderId);

        Task<Order> CreateOrder(Guid userId, CancellationToken cancellationToken = default );

        Task ClearOrder(Guid orderId, CancellationToken cancellationToken = default);
    }
}
