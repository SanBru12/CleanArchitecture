using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Domain.Common;

namespace Infrastructure.Persistence.Extensions
{
    public class AuditableEntityHelper
    {
        public static void SaveAuditableMetadata(ChangeTracker changeTracker, Guid? userGuid)
        {
            foreach (var entry in changeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt ??= DateTime.UtcNow;
                    entry.Entity.CreatedBy ??= userGuid ?? Guid.Empty;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy ??= userGuid ?? Guid.Empty;
                }
            }
        }

        public static Guid? GetUserIdFromContext(IHttpContextAccessor httpContextAccessor)
        {
            var claim = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier || c.Type == "sub" || c.Type == "userId" ||
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            return Guid.TryParse(claim?.Value, out var guid) ? guid : (Guid?)null;
        }
    }
}
