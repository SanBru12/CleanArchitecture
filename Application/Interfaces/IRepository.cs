namespace Application.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T model);
        Task UpdateAsync(T model);
        Task DeleteAsync(int id);
    }
}
