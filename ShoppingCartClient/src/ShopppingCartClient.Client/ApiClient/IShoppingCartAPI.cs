using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShoppingCartClient.Client.ApiClient.Models;

namespace ShoppingCartClient.Client.ApiClient
{
    public interface IShoppingCartApi
    {
        Uri BaseUri { get; }

        Task<OrderItem> GetOrderItemAsync(Guid orderItemId, CancellationToken cancellationToken = default);

        Task<bool> DeleteOrderItemAsync(Guid orderItemId, CancellationToken cancellationToken = default);

        Task<OrderItem> CreateOrderItemAsync(Guid orderId, Guid productId, int quantity = 1, CancellationToken cancellationToken = default);

        Task<OrderItem> UpdateOrderItemAsync(Guid orderItemId, int quantity, CancellationToken cancellationToken = default);

        Task<IList<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default);

        Task<Order> GetOrderAsync(Guid orderId, CancellationToken cancellationToken = default);

        Task<bool> ClearOrderAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IList<Order>> GetCustomerOrdersAsync(Guid customerId, CancellationToken cancellationToken = default);

        Task<Order> CreateOrderAsync(Guid customerId, CancellationToken cancellationToken = default);

    }
}
