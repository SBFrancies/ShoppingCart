using ShoppingCartApi.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net;
using System.Net.Http;
using ShoppingCartApi.Common.Models;

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
        public async Task GetEndpointReturnsSuccessAndCorrectContentType()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/v1/orderitems/43E4BA9F-5D82-4414-9702-5CA3DEF48168");

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

        //[Fact]
        //public async Task PostEndpointReturnsSuccessAndCorrectContentType()
        //{
        //    var client = _factory.CreateClient();

        //    var response = await client.PostAsync("/v1/orderitems/4B61ECAB-117A-4CBD-AFF0-B56573C82083/B8C7A7FA-3C84-4A4A-BD9F-36BCA1A9AD13/2", null);

        //    response.EnsureSuccessStatusCode();
        //    Assert.Equal("application/json; charset=utf-8",
        //        response.Content.Headers.ContentType.ToString());
        //}

        [Fact]
        public async Task PostReturns400WhenQuantityLessThan1()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync("/v1/orderitems/4B61ECAB-117A-4CBD-AFF0-B56573C82083/B8C7A7FA-3C84-4A4A-BD9F-36BCA1A9AD13/0", null);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task PostReturns404WhenOrderNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync($"/v1/orderitems/{Guid.NewGuid()}/B8C7A7FA-3C84-4A4A-BD9F-36BCA1A9AD13/2", null);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task PostReturns404WhenProductNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync($"/v1/orderitems/4B61ECAB-117A-4CBD-AFF0-B56573C82083/{Guid.NewGuid()}/2", null);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task PutEndpointReturnsSuccessAndCorrectContentType()
        {
            var client = _factory.CreateClient();

            var response = await client.PutAsync("/v1/orderitems/43E4BA9F-5D82-4414-9702-5CA3DEF48168/2", null);

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task PutReturns400WhenQuantityLessThan1()
        {
            var client = _factory.CreateClient();

            var response = await client.PutAsync("/v1/orderitems/29E4485C-483B-4AE7-8643-97A5D0D8A784/0", null);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task PutReturns404WhenOrderItemNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.PutAsync($"/v1/orderitems/{Guid.NewGuid()}/2", null);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        //[Fact]
        //public async Task DeleteEndpointReturnsSuccessAndCorrectContentType()
        //{
        //    var client = _factory.CreateClient();

        //    var response = await client.DeleteAsync("/v1/orderitems/29E4485C-483B-4AE7-8643-97A5D0D8A784/");

        //    response.EnsureSuccessStatusCode();
        //    Assert.Equal("application/json; charset=utf-8",
        //        response.Content.Headers.ContentType.ToString());
        //}

        [Fact]
        public async Task DeleteReturns404WhenOrderItemNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"/v1/orderitems/{Guid.NewGuid()}/");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
