using Application.Interfaces.Repositories;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Infrastructure.Services.Repository
{
    public class Repository<T>(AppDbContext context) : IRepository<T> where T : class
    {
        private readonly AppDbContext _context = context;

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

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>?  orderBy = null, params Expression<Func<T, object>>[] includes)
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

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> filter) => await _context.Set<T>().Where(filter).ToListAsync();

        public async Task<T> GetByIdAsync(Guid id) => await _context.Set<T>().FindAsync(id) ?? default!;

        public void Add(T entity) => _context.Set<T>().Add(entity);

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);
    }
}
