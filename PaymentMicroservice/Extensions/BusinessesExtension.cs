using Microsoft.Extensions.DependencyInjection;
using PaymentMicroservice.Businesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Extensions
{
    public static class BusinessesExtension
    {
        public static void Businesses(this IServiceCollection services)
        {
            services.AddScoped<PaymentBusiness>();
        }
    }
}
