using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentApp.Exceptions;
using AppointmentApp.Interfaces;

namespace AppointmentApp.Repositories
{
    public abstract class Repository<K,T> : IRepository<K,T> where T : class
    {
        protected List<T> _items = new List<T>();
        public abstract K GenerateId();
        public abstract T GetById(K id);
        public T Add(T item) 
        {
            K newId = GenerateId();
            var property = typeof(T).GetProperty("Id");
            if (property != null)
            {
                property.SetValue(item, newId);
            }
            _items.Add(item);
            return item;
        }

        public ICollection<T> GetAll() 
        { 
            if(_items == null || _items.Count == 0)
            {
                throw new EmptyEntityException("No data Found");
            }
            return _items;
        }

        
    }
}
