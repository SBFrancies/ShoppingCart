using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShoppingCartClient.Client.ApiClient;
using ShoppingCartClient.Client.ApiClient.Models;

namespace ShoppingCartClient.Client
{
    public class OrderManager
    {
        private readonly IShoppingCartApi _apiClient;

        public OrderManager(IShoppingCartApi apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public async Task<Order> CreateOrderAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _apiClient.CreateOrderAsync(customerId, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception("Error creating order, see inner exception for details", exception);
            }
        }

        public async Task<bool> ClearOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _apiClient.ClearOrderAsync(orderId, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error clearing order {orderId}, see inner exception for details", exception);
            }
        }

        public async Task<Order> GetOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _apiClient.GetOrderAsync(orderId, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error fetching order {orderId}, see inner exception for details", exception);
            }
        }

        public async Task<IList<Order>> GetOrdersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _apiClient.GetAllOrdersAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception("Error fetching orders, see inner exception for details", exception);
            }
        }

        public async Task<IList<Order>> GetOrdersAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _apiClient.GetCustomerOrdersAsync(customerId, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error fetching orders for customer {customerId}, see inner exception for details", exception);
            }
        }

    }
}
