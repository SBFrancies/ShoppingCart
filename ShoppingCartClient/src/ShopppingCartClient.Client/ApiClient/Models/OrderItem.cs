// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

using Newtonsoft.Json;

namespace ShoppingCartClient.Client.ApiClient.Models
{
    public partial class OrderItem
    {
        public OrderItem(Product product = default, int quantity = default)
        {
            Product = product;
            Quantity = quantity;
        }

        [JsonProperty("product")]
        public Product Product { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
