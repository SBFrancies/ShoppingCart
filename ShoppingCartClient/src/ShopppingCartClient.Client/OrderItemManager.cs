using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShoppingCartClient.Client.ApiClient;
using ShoppingCartClient.Client.ApiClient.Models;

namespace ShoppingCartClient.Client
{
    public class OrderItemManager
    {
        private readonly IShoppingCartApi _apiClient;

        public OrderItemManager(IShoppingCartApi apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public async Task<OrderItem> CreateOrderItemAsync(Guid orderId, Guid productId, int quantity = 1, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _apiClient.CreateOrderItemAsync(orderId, productId, quantity, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception("Error creating order item, see inner exception for details", exception);
            }
        }

        public async Task<bool> DeleteOrderItemAsync(Guid orderItemId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _apiClient.DeleteOrderItemAsync(orderItemId, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error deleting order item {orderItemId}, see inner exception for details", exception);
            }
        }

        public async Task<OrderItem> GetOrderItemAsync(Guid orderItemId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _apiClient.GetOrderItemAsync(orderItemId, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error fetching order item {orderItemId}, see inner exception for details", exception);
            }
        }

        public async Task<OrderItem> UpdateOrderItemQuantitytAsync(Guid orderItemId, int quantity, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _apiClient.UpdateOrderItemAsync(orderItemId, quantity, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error updating order item {orderItemId}, see inner exception for details", exception);
            }
        }

        public async Task<OrderItem> IncrementOrderItemQuantitytAsync(Guid orderItemId, CancellationToken cancellationToken = default)
        {
            try
            {
                OrderItem orderItem = await _apiClient.GetOrderItemAsync(orderItemId, cancellationToken);
                return await _apiClient.UpdateOrderItemAsync(orderItemId, ++orderItem.Quantity, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error incrementing order item {orderItemId} quantity, see inner exception for details", exception);
            }
        }

        public async Task<OrderItem> DecrementOrderItemQuantitytAsync(Guid orderItemId, CancellationToken cancellationToken = default)
        {
            try
            {
                OrderItem orderItem = await _apiClient.GetOrderItemAsync(orderItemId, cancellationToken);
                return await _apiClient.UpdateOrderItemAsync(orderItemId, --orderItem.Quantity, cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error decrementing order item {orderItemId} quantity, see inner exception for details", exception);
            }
        }
    }
}
