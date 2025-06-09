using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Extensions
{
    public static class AuditableEntityConfiguration
    {
        public static void ConfigureAuditableEntities(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (!typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                    continue;

                var entity = builder.Entity(entityType.ClrType);

                entity.Property("CreatedBy")
                      .HasColumnType("uniqueidentifier")
                      .IsRequired(false);

                entity.Property("UpdatedBy")
                      .HasColumnType("uniqueidentifier")
                      .IsRequired(false);

                entity.Property("CreatedAt")
                      .HasColumnType("datetime2")
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.Property("UpdatedAt")
                      .HasColumnType("datetime2")
                      .HasDefaultValueSql("GETUTCDATE()")
                      .ValueGeneratedOnUpdate();
            }
        }
    }
}
