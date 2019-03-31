using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCartApi.Data;

namespace ShoppingCartApi.UnitTests.DbFixtures
{
    public class ShoppingCartFixtureContext : IDisposable
    {
        private ShoppingCartEfDatabaseFixture _shoppingCartEfDatabaseFixture; 

        public ShoppingCartDbContext Context => _shoppingCartEfDatabaseFixture?.Context ?? CreateContext();

        public ShoppingCartDbContext CreateContext()
        {
            _shoppingCartEfDatabaseFixture = new ShoppingCartEfDatabaseFixture();
            return _shoppingCartEfDatabaseFixture.CreateContext();
        }

        public void Dispose()
        {
            _shoppingCartEfDatabaseFixture.Dispose();
        }
    }
}
