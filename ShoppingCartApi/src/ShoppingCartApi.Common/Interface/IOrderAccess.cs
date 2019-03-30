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

        IReadOnlyCollection<Order> GetCustomerOrders(Guid customerId);

        Order GetOrder(Guid orderId);

        Task<Order> CreateOrderAsync(Guid customerId, CancellationToken cancellationToken = default );

        Task ClearOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
    }
}
