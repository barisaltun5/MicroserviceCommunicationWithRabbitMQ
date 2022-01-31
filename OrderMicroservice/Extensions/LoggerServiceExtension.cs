using Microsoft.Extensions.DependencyInjection;
using OrderMicroservice.Contracts.Logger;
using OrderMicroservice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.Extensions
{
    public static class LoggerServiceExtension
    {
        public static void ConfigureLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
