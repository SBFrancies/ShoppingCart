using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using ShoppingCartApi.Api;
using Xunit;

namespace ShoppingCartClient.IntegrationTests
{
    public class OrdersControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public OrdersControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("v1/orders")]
        [InlineData("v1/orders/14EE82E7-3D8F-422C-9D6E-416710564141")]
        [InlineData("v1/orders/780D165E-FE91-4162-A90E-DBFE5E9A2B35/customer")]
        public async Task GetEndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetReturns404WhenNoOrderFound()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/v1/orders/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetReturns404WhenCustomerNotFoundOrHasNoOrders()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/v1/orders/{Guid.NewGuid()}/customer");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostEndpointReturnsSuccessAndCorrectContentType()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync("/v1/orders/780D165E-FE91-4162-A90E-DBFE5E9A2B35", null);

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task PostReturns404WhenCustomerNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync($"/v1/orders/{Guid.NewGuid()}", null);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task DeleteEndpointReturnsSuccess()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/v1/orders/1BA79335-00A9-49D2-BEE4-D603AA10AAF6");

            response.EnsureSuccessStatusCode();
        }
    }
}
