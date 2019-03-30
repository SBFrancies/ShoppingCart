using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCartClient.Client.ApiClient;

namespace ShoppingCartClient.Client
{
    public class OrderItemManager
    {
        private readonly IShoppingCartApi _apiClient;

        public OrderItemManager(IShoppingCartApi apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }
    }
}
