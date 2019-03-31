using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCartApi.UnitTests.DbFixtures;
using Xunit;

namespace ShoppingCartApi.UnitTests.AccessTests
{
    public class OrderItemAccessTests : ShoppingCartFixtureContext, IClassFixture<ShoppingCartEfDatabaseFixture>
    {
    }
}
