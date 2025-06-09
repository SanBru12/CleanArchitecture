using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Infrastructure.Repository.Extensions;
using Infrastructure.Repository.Search;
using Microsoft.EntityFrameworkCore;
using Models.Responses;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Base
{
    public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<T>> SearchAsync(SearchQuery request)
        {
            IQueryable<T> query = _context.Set<T>();

            query = RepositoryExtensions<T>.ApplyIncludes(query, request.Includes);

            query = RepositoryExtensions<T>.ApplyFilter(query, request.Filter);

            query = RepositoryExtensions<T>.ApplyOrder(query, request.OrderBy, request.OrderByDirection);

            query = RepositoryExtensions<T>.ApplyPagination(query, request.Skip, request.Take);

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id) => await _context.Set<T>().FindAsync(id) ?? default!;

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
