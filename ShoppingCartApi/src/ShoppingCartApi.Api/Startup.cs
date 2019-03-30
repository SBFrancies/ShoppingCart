using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ShoppingCartApi.Access;
using ShoppingCartApi.Common.Interface;
using ShoppingCartApi.Data;
using Swashbuckle.AspNetCore.Swagger;

namespace ShoppingCartApi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("Main");
            string migrationsAssembly = typeof(ShoppingCartDbContextFactory).GetTypeInfo().Assembly.GetName().Name;

            MigrateDatabase(connectionString, migrationsAssembly);

            void ConfigureDbContext(DbContextOptionsBuilder builder)
            {
                ConfigureDbOptions(builder, connectionString, migrationsAssembly);
            }

            services.AddDbContext<ShoppingCartDbContext>(ConfigureDbContext, ServiceLifetime.Transient, ServiceLifetime.Singleton);
            services.TryAddSingleton<Func<ShoppingCartDbContext>>(s => s.GetRequiredService<ShoppingCartDbContext>);

            services.TryAddSingleton<IOrderAccess, OrderAccess>();
            services.TryAddSingleton<IOrderItemAccess, OrderItemAccess>();

            services.AddMvcCore()
                .AddJsonFormatters(settings =>
                {
                    settings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                c.SwaggerDoc("v1", new Info { Title = "Shopping Cart API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePages()
                .UseHttpsRedirection()
                .UseMvc()
                .UseStaticFiles()
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping Cart API v1");
                });
        }

        private void ConfigureDbOptions(DbContextOptionsBuilder builder, string connectionString, string migrationsAssembly)
        {
            builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
        }

        private void MigrateDatabase(string connectionString, string migrationsAssembly)
        {
            var builder = new DbContextOptionsBuilder<ShoppingCartDbContext>();
            ConfigureDbOptions(builder, connectionString, migrationsAssembly);

            using (var context = new ShoppingCartDbContext(builder.Options))
            {
                context.Database.Migrate();
            }
        }
    }
}
