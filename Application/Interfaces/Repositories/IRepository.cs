using Models.Responses;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, QueryParameters? parameters = null, params Expression<Func<T, object>>[] includes);
    }
}
