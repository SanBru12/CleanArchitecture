using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Text.Json;

namespace Infrastructure.Persistence.Extensions
{
    public class AuditLogTracker
    {
        private static readonly Type[] ExcludedTypes = { typeof(AuditLog), typeof(ErrorLog) };

        public static List<AuditLog> TrackChanges(ChangeTracker changeTracker, IHttpContextAccessor contextAccessor)
        {
            var result = new List<AuditLog>();

            foreach (var entry in changeTracker.Entries())
            {
                if (ExcludedTypes.Contains(entry.Entity.GetType()))
                    continue;

                result.Add(new AuditLog
                {
                    TableName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    UserId = contextAccessor.HttpContext?.User?.Identity?.Name ?? "System",
                    RemoteIpAddress = contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown",
                    KeyValues = GetPrimaryKey(entry),
                    OldValues = ShouldIncludeOld(entry.State) ? Serialize(entry, false) : "",
                    NewValues = ShouldIncludeNew(entry.State) ? Serialize(entry, true) : ""
                });
            }

            return result;
        }

        private static string GetPrimaryKey(EntityEntry entry) =>
            entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue?.ToString() ?? "";

        private static bool ShouldIncludeOld(EntityState state) =>
            state == EntityState.Modified || state == EntityState.Deleted;

        private static bool ShouldIncludeNew(EntityState state) =>
            state == EntityState.Added || state == EntityState.Modified;

        private static string Serialize(EntityEntry entry, bool isNew)
        {
            var dict = entry.Properties.ToDictionary(
                prop => prop.Metadata.Name,
                prop => prop.CurrentValue ?? "");

            return JsonSerializer.Serialize(dict);
        }
    }
}
