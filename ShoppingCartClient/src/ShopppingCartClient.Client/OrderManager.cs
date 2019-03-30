using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
