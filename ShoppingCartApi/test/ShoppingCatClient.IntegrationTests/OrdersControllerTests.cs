using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using ShoppingCartApi.Api;
using Xunit;

namespace ShoppingCartClient.IntegrationTests
{
    public class OrdersControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
    }
}
