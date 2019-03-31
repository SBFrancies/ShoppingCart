using ShoppingCartApi.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCartApi.UnitTests.DbFixtures
{
    public class ShoppingCartEfDatabaseFixture
    {
        private readonly string _connectionString =
            $@"Server=(LocalDB)\MSSQLLocalDB;Database=shopping-db-test{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true";

        public ShoppingCartEfDatabaseFixture()
        {
            Context = CreateContext();
            Context.Database.Migrate();
        }

        public ShoppingCartDbContext Context { get; set; }

        public ShoppingCartDbContext CreateContext()
        {
            return new ShoppingCartDbContextFactory().CreateDbContext(new[] { _connectionString });
        }

        public string ConnectionString => _connectionString;

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }
    }
}
