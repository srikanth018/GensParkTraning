using AppointmentApp.Contexts;
using AppointmentApp.Interfaces;

namespace AppointmentApp.Repositories
{
    public abstract class Repository<K, T> : IRepository<K, T> where T : class
    {
        protected readonly ClinicContext _clinicContext;

        public Repository(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }

        public abstract Task<IEnumerable<T>>? GetAll();
        public abstract Task<T>? GetById(K key);

        public async Task<T> Add(T item)
        {
            _clinicContext.Add(item);
            await _clinicContext.SaveChangesAsync();
            return item;
        }

        public async Task<T>? Delete(K key)
        {

            var item = await GetById(key);
            if (item != null)
            {
                _clinicContext.Remove(item);
                await _clinicContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for deleting");
        }

        public async Task<T>? Update(K key, T item)
        {
            var newitem = await GetById(key);
            if (item != null)
            {
                _clinicContext.Entry(newitem).CurrentValues.SetValues(item);
                await _clinicContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for updation");
        }
    }
}