using Newtonsoft.Json;

namespace ShoppingCartClient.Client.ApiClient.Models
{
    public class Product
    {
        public Product(string name = default, string description = default)
        {
            Name = name;
            Description = description;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}