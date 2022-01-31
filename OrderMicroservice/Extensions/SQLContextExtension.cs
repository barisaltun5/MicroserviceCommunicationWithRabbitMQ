using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderMicroservice.DAL;
using System;

namespace OrderMicroservice.Extensions
{
    public static class SQLContextExtension
    {
        public static void ConfigureSQLContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            var config = configuration.GetConnectionString("OrderMicroserviceContext");
            services.AddDbContext<OrderMicroserviceContext>(o => o.UseSqlServer(connectionString, b =>
            {
                b.MigrationsAssembly("OrderMicroservice");
            }));
        }
    }
}
