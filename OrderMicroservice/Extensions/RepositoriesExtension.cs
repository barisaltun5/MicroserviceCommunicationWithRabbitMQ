using Microsoft.Extensions.DependencyInjection;
using OrderMicroservice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.Extensions
{
    public static class RepositoriesExtension
    {
        public static void Repositories(this IServiceCollection services)
        {
            services.AddScoped<OrderRepository>();
        }
    }
}
