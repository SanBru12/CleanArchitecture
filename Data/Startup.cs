using Data.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class Startup
    {
        public static IServiceCollection AddApplicationDbConnection(this IServiceCollection services, string SqlConnection)
        {
            if (string.IsNullOrEmpty(SqlConnection))
                throw new ArgumentNullException(nameof(SqlConnection));

             services.AddDbContext<AppDbContext>(options => options.UseSqlServer(SqlConnection));

            return services;
        }
    }
}
