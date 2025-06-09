using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Repository.Extensions
{
    public class RepositoryExtensions<T> where T : class
    {
        public static IQueryable<T> ApplyIncludes(IQueryable<T> query, string[]? includes) 
        {
            if (includes == null) return query;

            foreach (var include in includes)
                query = query.Include(include);

            return query;
        }

        public static IQueryable<T> ApplyFilter(IQueryable<T> query, string? filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? combined = null;

            foreach (var prop in typeof(T).GetProperties().Where(p => p.PropertyType == typeof(string)))
            {
                var propExpr = Expression.Property(parameter, prop);
                var searchExpr = Expression.Constant(filter);
                var containsExpr = Expression.Call(propExpr, nameof(string.Contains), null, searchExpr);

                combined = combined == null ? containsExpr : Expression.OrElse(combined, containsExpr);
            }

            if (combined == null) return query;

            var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
            return query.Where(lambda);
        }

        public static IQueryable<T> ApplyOrder(IQueryable<T> query, string? orderBy, string? direction)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query;

            var entityType = typeof(T);
            var property = entityType.GetProperty(orderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null) return query;

            var parameter = Expression.Parameter(entityType, "x");
            var propertyAccess = Expression.Property(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            string methodName = direction?.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";

            var resultExp = Expression.Call(typeof(Queryable), methodName,
                new Type[] { entityType, property.PropertyType },
                query.Expression, Expression.Quote(orderByExp));

            return query.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> ApplyPagination(IQueryable<T> query, int? skip, int? take)
        {
            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query;
        }
    }
}
