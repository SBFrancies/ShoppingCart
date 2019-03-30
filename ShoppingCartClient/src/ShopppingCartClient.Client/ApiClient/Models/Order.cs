using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShoppingCartClient.Client.ApiClient.Models
{
    public class Order
    {
        public Order(System.Guid? id = default, IList<OrderItem> items = default)
        {
            Id = id;
            Items = items;
        }

        [JsonProperty("id")]
        public System.Guid? Id { get; set; }

        [JsonProperty("items")]
        public IList<OrderItem> Items { get; set; }

    }
}
