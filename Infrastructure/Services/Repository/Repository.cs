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
