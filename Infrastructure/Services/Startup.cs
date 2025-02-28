using Application.Interfaces.Repositories;
using Infrastructure.Services.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Services
{
    public static class Startup
    {
        public static IServiceCollection AddServiceSettings(this IServiceCollection services)
        {
            // agregamos el servicio de repositorio dinamico
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // agregar aca el resto de servicios

            return services;
        }

    }
}
