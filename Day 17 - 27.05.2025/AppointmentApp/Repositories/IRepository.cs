
using System.Collections.Generic;
namespace AppointmentApp.Repositories
{
    public interface IRepository<T, TKey> where T : class
    {
        IEnumerable<T>? GetAll();
        T? GetById(TKey id);
        T Add(T entity);
        T? Update(T entity);
        T? Delete(TKey id);
    }

}