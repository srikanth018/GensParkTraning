namespace BankApp.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        
    }
}