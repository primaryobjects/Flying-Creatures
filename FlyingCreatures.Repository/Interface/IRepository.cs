using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingCreatures.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> where);
        T Single(Func<T, bool> where);
        T First(Func<T, bool> where);

        void Delete(T entity);
        void Add(T entity);
        void SaveChanges();
    }
}
