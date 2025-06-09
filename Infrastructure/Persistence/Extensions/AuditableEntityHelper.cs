using Domain.Common;
using Domain.Common.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;

namespace Infrastructure.Persistence.Extensions
{
    public class AuditableEntityHelper
    {
        public static void SaveAuditableMetadata(ChangeTracker changeTracker, Guid? userGuid)
        {
            foreach (var entry in changeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = userGuid ?? Guid.Empty;
                    entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = userGuid ?? Guid.Empty;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = userGuid ?? Guid.Empty;
                }
            }

            foreach (var entry in changeTracker.Entries<ISoftDelete>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.DeletedOn = DateTime.UtcNow;
                    entry.Entity.DeletedBy = userGuid ?? Guid.Empty;
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
