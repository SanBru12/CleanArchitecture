using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess
{
    public static class Startup
    {
        public static IServiceCollection AddApplicationDbConnection(this IServiceCollection services, string SqlConnection)
        {
            if (string.IsNullOrEmpty(SqlConnection))
                throw new ArgumentNullException(nameof(SqlConnection));

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(SqlConnection));

            services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
                   .AddEntityFrameworkStores<AppDbContext>()
                   .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequiredUniqueChars = 0;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.AllowedForNewUsers = true;
            });

            return services;
        }
    }
}
