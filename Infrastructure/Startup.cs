using Infrastructure.AddApplicationSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.ApplicationSettings.Extensions;
using Data;

namespace Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationSettings(configuration);

            ProgramExtension programExtension = services.BuildServiceProvider().GetRequiredService<ProgramExtension>(); 

            services.AddApplicationDbConnection(programExtension.SqlConnection);

            return services;
        }
    }
}
