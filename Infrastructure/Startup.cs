using Infrastructure.AddApplicationSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.ApplicationSettings.Extensions;
using Infrastructure.DataAccess;
using Application;
using Infrastructure.Services;
using Api.Infrastructure.AutoMapper;

namespace Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationSettings(configuration);

            ProgramExtension programExtension = services.BuildServiceProvider().GetRequiredService<ProgramExtension>(); 

            services.AddApplicationDbConnection(programExtension.SqlConnection);

            services.AddConfigMediatR();

            services.AddServiceSettings();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }


    }
}
