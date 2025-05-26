using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Models.Responses;
using System.Linq.Expressions;

namespace Infrastructure.Services.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.FirstOrDefaultAsync().ConfigureAwait(false) ?? default!;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
                                                      Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy = null,
                                                      QueryParameters? parameters = null,
                                                      params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Filtro por Search string (aplicado a propiedades string de la entidad)
            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                var stringProps = typeof(T).GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                var parameter = Expression.Parameter(typeof(T), "x");
                Expression? orExpressions = null;

                foreach (var prop in stringProps)
                {
                    var propExpr = Expression.Property(parameter, prop);
                    var searchExpr = Expression.Constant(parameters.Search);
                    var containsExpr = Expression.Call(propExpr, nameof(string.Contains), null, searchExpr);
                    orExpressions = orExpressions == null ? containsExpr : Expression.OrElse(orExpressions, containsExpr);
                }

                if (orExpressions != null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(orExpressions, parameter);
                    query = query.Where(lambda);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Paginado
            if (parameters?.Page is > 0 && parameters.PageSize is > 0)
            {
                int skip = (parameters.Page.Value - 1) * parameters.PageSize.Value;
                query = query.Skip(skip).Take(parameters.PageSize.Value);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id) ?? default!;

        public async Task<T> Add(T modelo)
        {
            await _context.Set<T>().AddAsync(modelo);
            await _context.SaveChangesAsync();
            return modelo;
        }

        public async Task Update(T modelo)
        {
            _context.Set<T>().Update(modelo);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T modelo)
        {
            _context.Set<T>().Remove(modelo);
            await _context.SaveChangesAsync();
        }
    }
}
