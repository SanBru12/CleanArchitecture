using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Models.Entity;
using Data.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.Models;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Configuration;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // DbSets para las entidades personalizadas
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public DbSet<ErrorLog> ErrorLogs { get; set; } = null!;
        public DbSet<Tenant> Tenants { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AuditLog>().ToTable("AuditLogs", SchemaNames.System);
            builder.Entity<ErrorLog>().ToTable("ErrorLogs", SchemaNames.System);
            builder.Entity<Tenant>().ToTable("Tenants", SchemaNames.MultiTenancy);

            // Configuración para Identity
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            builder.ConfigureAuditableEntities();
        }

        // Para guardar la fecha y quien creo el registro
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userGuid = AuditableEntityHelper.GetUserIdFromContext(_httpContextAccessor);

            AuditableEntityHelper.SaveAuditableMetadata(ChangeTracker, userGuid);

            var auditLogs = AuditLogTracker.TrackChanges(ChangeTracker, _httpContextAccessor);

            AuditLogs.AddRange(auditLogs);

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
