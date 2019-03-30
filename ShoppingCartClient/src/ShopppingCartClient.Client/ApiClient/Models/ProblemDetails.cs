using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShoppingCartClient.Client.ApiClient.Models
{

    public class ProblemDetails
    {
        public ProblemDetails(
            IDictionary<string, object> additionalProperties = default,
            string type = default,
            string title = default,
            int? status = default,
            string detail = default,
            string instance = default)
        {
            AdditionalProperties = additionalProperties;
            Type = type;
            Title = title;
            Status = status;
            Detail = detail;
            Instance = instance;
        }

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public int? Status { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("instance")]
        public string Instance { get; set; }

    }
}
