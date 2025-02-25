using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Models.Entity;
using Data.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;

namespace Infrastructure.Application.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext( DbContextOptions<AppDbContext> options,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // DbSets para las entidades personalizadas

        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public DbSet<Tenant> Tenants { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AuditLog>().ToTable("AuditLogs");

            builder.Entity<Tenant>().ToTable("Tenants");

            // Configuración para Identity
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Configurar CreatedBy y UpdatedBy como uniqueidentifier en todas las tablas
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType)
                       .Property("CreatedBy")
                       .HasColumnType("uniqueidentifier")
                       .IsRequired(false);

                    builder.Entity(entityType.ClrType)
                        .Property("UpdatedBy")
                        .HasColumnType("uniqueidentifier")
                        .IsRequired(false);

                    builder.Entity(entityType.ClrType)
                        .Property("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("NULL");

                    builder.Entity(entityType.ClrType)
                        .Property("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("NULL");
                }
            }

            // Configuración de fechas automáticas
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType)
                        .Property("CreatedAt")
                        .HasDefaultValueSql("GETUTCDATE()");

                    builder.Entity(entityType.ClrType)
                        .Property("UpdatedAt")
                        .HasDefaultValueSql("GETUTCDATE()")
                        .ValueGeneratedOnUpdate();
                }
            }
        }

        // Para guardar la fecha y quien creo el registro
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TrackChanges();
            SaveAuditableEntity();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SaveAuditableEntity()
        {
            Guid? userGuid = null;
            if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c =>
                        c.Type == ClaimTypes.NameIdentifier ||
                        c.Type == "sub" ||
                        c.Type == "userId" ||
                        c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var parsedUserId))
                {
                    userGuid = parsedUserId;
                }
            }

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt ??= DateTime.UtcNow;
                    entry.Entity.CreatedBy ??= userGuid ?? Guid.Empty;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userGuid;
                }
            }

  
        }

        private void TrackChanges()
        {
            var auditEntries = new List<AuditLog>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog) continue;

                var auditLog = new AuditLog
                {
                    TableName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    UserId = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System",
                    RemoteIpAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown",
                    KeyValues = GetPrimaryKey(entry),
                    OldValues = entry.State == EntityState.Modified || entry.State == EntityState.Deleted ? GetValues(entry, false) : "",
                    NewValues = entry.State == EntityState.Added || entry.State == EntityState.Modified ? GetValues(entry, true) : ""
                };

                auditEntries.Add(auditLog);
            }

            AuditLogs.AddRange(auditEntries);
        }

        private static string GetPrimaryKey(EntityEntry entry)
        {
            var key = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
            return key != null ? key.CurrentValue?.ToString() ?? "" : "";
        }

        private static string GetValues(EntityEntry entry, bool isNew)
        {
            var values = new Dictionary<string, object>();

            foreach (var prop in entry.Properties)
            {
                if (isNew)
                    values[prop.Metadata.Name] = prop.CurrentValue is null ? "" : prop.CurrentValue;
                else
                    values[prop.Metadata.Name] = prop.CurrentValue is null ? "" : prop.CurrentValue;
            }

            return System.Text.Json.JsonSerializer.Serialize(values);
        }
    }
}
