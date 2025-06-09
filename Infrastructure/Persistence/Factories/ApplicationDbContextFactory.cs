using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Factories
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // ⚠️ Reemplazá esto con tu cadena real
            var connectionString = "PC\\SQLEXPRESS; Database = CleanArchitecture ; User = sa; Password = esea; TrustServerCertificate = true";
            optionsBuilder.UseSqlServer(connectionString);

            // 🔧 Se puede pasar un IHttpContextAccessor dummy si es obligatorio
            var httpContextAccessor = new HttpContextAccessor();

            return new ApplicationDbContext(optionsBuilder.Options, httpContextAccessor);
        }
    }
}
