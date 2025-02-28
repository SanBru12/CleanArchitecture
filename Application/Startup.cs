using Microsoft.Extensions.DependencyInjection;
using System;

namespace Application
{
    public static class Startup
    {
        public static IServiceCollection AddConfigMediatR(this IServiceCollection services)
        {
            services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssembly(typeof(Startup).Assembly);
            });

            return services;
        }
    }
}
