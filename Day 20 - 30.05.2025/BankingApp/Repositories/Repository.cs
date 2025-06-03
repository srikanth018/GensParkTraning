using BankApp.Contexts;
using BankApp.Interfaces;

namespace BankApp.Repositories
{
    public abstract class Repository<K, T> : IRepository<K, T> where T : class
    {
        protected readonly BankingContext _bankingContext;

        public Repository(BankingContext bankingContext)
        {
            _bankingContext = bankingContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }

            _bankingContext.Add(entity);
            await _bankingContext.SaveChangesAsync();
            return entity;
        }

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<T?> GetByIdAsync(int id);
    }
}