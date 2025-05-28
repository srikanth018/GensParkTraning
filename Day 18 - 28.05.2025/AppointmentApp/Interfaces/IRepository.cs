
using System.Collections.Generic;
namespace AppointmentApp.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        Task<IEnumerable<T>>? GetAll();
        Task<T>? GetById(K key);
        Task<T> Add(T item);
        Task<T>? Update(K key,T item);
        Task<T>? Delete(K key);
    }

}