using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentMicroservice.DAL;
using System;

namespace PaymentMicroservice.Extensions
{
    public static class SQLContextExtension
    {
        public static void ConfigureSQLContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            //var config = configuration.GetConnectionString("PaymentMicroserviceContext");
            services.AddDbContext<PaymentMicroserviceContext>(o => o.UseSqlServer(connectionString, b =>
            {
                b.MigrationsAssembly("PaymentMicroservice");
            }));
        }
    }
}
