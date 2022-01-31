using Microsoft.Extensions.DependencyInjection;
using PaymentMicroservice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Extensions
{
    public static class RepositoriesExtension
    {
        public static void Repositories(this IServiceCollection services)
        {
            services.AddScoped<PaymentRepository>();
        }
    }
}
