using Application.Interfaces.Repositories;
using Infrastructure.Repository.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Repository
{
    public static class Startup
    {
        public static IServiceCollection AddServiceSettings(this IServiceCollection services)
        {
            // agregamos el servicio de repositorio dinamico
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            // agregar aca el resto de servicios

            return services;
        }

    }
}
