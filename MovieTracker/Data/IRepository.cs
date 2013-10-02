using System;
using System.Collections.Generic;

namespace MovieTracker.Data
{
    public interface IRepository<T> where T : class
    {
        void Save();
        IEnumerable<T> Find(Func<T, bool> filter);
        void Delete(T model);
        T Add(T model);
        IEnumerable<T> GetAll();
    }
}

