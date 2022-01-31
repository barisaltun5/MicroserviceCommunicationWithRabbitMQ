using Microsoft.Extensions.DependencyInjection;
using PaymentMicroservice.Contracts.Logger;
using PaymentMicroservice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Extensions
{
    public static class LoggerServiceExtension
    {
        public static void ConfigureLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
