using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ShoppingCartApi.Data
{
    public class ShoppingCartDbContextFactory : IDesignTimeDbContextFactory<ShoppingCartDbContext>
    {
        private const string ConnectionString =
            @"Server=(LocalDB)\MSSQLLocalDB;Database=shopping-db;Trusted_Connection=True";

        public ShoppingCartDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShoppingCartDbContext>();
            optionsBuilder.UseSqlServer(args.Length > 0 ? args[0] : ConnectionString);

            return new ShoppingCartDbContext(optionsBuilder.Options);
        }
    }
}
