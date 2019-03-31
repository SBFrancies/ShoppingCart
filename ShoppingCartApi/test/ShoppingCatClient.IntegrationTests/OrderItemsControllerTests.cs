using ShoppingCartApi.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net;

namespace ShoppingCartClient.IntegrationTests
{
    public class OrderItemsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public OrderItemsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetEndpointsReturnSuccessAndCorrectContentType()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/v1/orderitems/29E4485C-483B-4AE7-8643-97A5D0D8A784");

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetReturns404WhenNoOrderItemFound()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/v1/orderitems/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
