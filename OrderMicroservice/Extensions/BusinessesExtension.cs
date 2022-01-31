using Microsoft.Extensions.DependencyInjection;
using OrderMicroservice.Businesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.Extensions
{
    public static class BusinessesExtension
    {
        public static void Businesses(this IServiceCollection services)
        {
            services.AddScoped<OrderBusiness>();
        }
    }
}
