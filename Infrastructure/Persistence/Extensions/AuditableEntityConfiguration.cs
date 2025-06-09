using Domain.Common;
using Domain.Common.Contracts;
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
                      .IsRequired();

                entity.Property("LastModifiedBy")
                      .HasColumnType("uniqueidentifier")
                      .IsRequired();

                entity.Property("CreatedOn")
                      .HasColumnType("datetime2")
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.Property("LastModifiedOn")
                      .HasColumnType("datetime2")
                      .HasDefaultValueSql("GETUTCDATE()");
            }
        }
    }
}
