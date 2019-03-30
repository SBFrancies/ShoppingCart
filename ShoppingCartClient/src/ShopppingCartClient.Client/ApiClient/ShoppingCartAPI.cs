using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ShoppingCartClient.Client.ApiClient.Models;
using ShoppingCartClient.Client.Exceptions;

namespace ShoppingCartClient.Client.ApiClient
{
    public class ShoppingCartApi : IShoppingCartApi
    {
        private readonly HttpClient _client = new HttpClient();
        public Uri BaseUri { get;  }

        public ShoppingCartApi(string baseUri)
        {
            BaseUri = new Uri(baseUri);
        }

        public async Task<OrderItem> GetOrderItemAsync(Guid orderItemId, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, $"v1/orderitems/{orderItemId}");
            HttpResponseMessage result = await _client.GetAsync(uri, cancellationToken);

            switch(result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await result.Content.ReadAsAsync<OrderItem>();

                case HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }

        public async Task<bool> DeleteOrderItemAsync(Guid orderItemId, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, $"v1/orderitems/{orderItemId}");
            HttpResponseMessage result = await _client.DeleteAsync(uri, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;

                case HttpStatusCode.NotFound:
                    return false;

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }

        public async Task<OrderItem> CreateOrderItemAsync(Guid orderId, Guid productId, int quantity = 1, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, $"v1/orderitems/{orderId}/{productId}/{quantity}");
            HttpResponseMessage result = await _client.PostAsync(uri, null, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await result.Content.ReadAsAsync<OrderItem>();

                case HttpStatusCode.NotFound:
                    return null;

                case HttpStatusCode.BadRequest:
                    ProblemDetails problem = await result.Content.ReadAsAsync<ProblemDetails>();
                    throw new ProblemDetailsException(problem);

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }

        public async Task<OrderItem> UpdateOrderItemAsync(Guid orderItemId, int quantity, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, $"v1/orderitems/{orderItemId}/{quantity}");
            HttpResponseMessage result = await _client.PutAsync(uri, null, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await result.Content.ReadAsAsync<OrderItem>();

                case HttpStatusCode.NotFound:
                    return null;

                case HttpStatusCode.BadRequest:
                    ProblemDetails problem = await result.Content.ReadAsAsync<ProblemDetails>();
                    throw new ProblemDetailsException(problem);

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }

        public async Task<IList<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, $"v1/orders");
            HttpResponseMessage result = await _client.GetAsync(uri, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await result.Content.ReadAsAsync<List<Order>>();

                case HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }

        public async Task<Order> GetOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, $"v1/orders/{orderId}");
            HttpResponseMessage result = await _client.GetAsync(uri, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await result.Content.ReadAsAsync<Order>();

                case HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }

        public async Task<bool> ClearOrderAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, $"v1/orders/{id}");
            HttpResponseMessage result = await _client.DeleteAsync(uri, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }

        public async Task<IList<Order>> GetCustomerOrdersAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, $"v1/orders/{customerId}/customer");
            HttpResponseMessage result = await _client.GetAsync(uri, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await result.Content.ReadAsAsync<List<Order>>();

                case HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }

        public async Task<Order> CreateOrderAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, $"v1/orders/{customerId}");
            HttpResponseMessage result = await _client.PostAsync(uri, null, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await result.Content.ReadAsAsync<Order>();

                case HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }
    }
}
