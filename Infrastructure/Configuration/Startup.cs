using Infrastructure.Configuration.Extensions;
using Infrastructure.Configuration.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.AddApplicationSettings
{
    public static class Startup
    {
        public static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EnvironmentSettings>(
                configuration.GetSection(EnvironmentSettings.SectionName));

            services.Configure<ConnectionStringsSettings>(
                configuration.GetSection(ConnectionStringsSettings.SectionName));

            services.AddSingleton<ProgramExtension>();

            return services;
        }
    }
}
