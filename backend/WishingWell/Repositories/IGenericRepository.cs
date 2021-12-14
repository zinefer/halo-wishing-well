using System.Collections.Generic;

namespace WishingWell.Repositories
{
    public interface IGenericRepository<T>
    where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(string id);

        void Insert(T obj);

        void Update(T obj);

        void Upsert(T obj);

        void Delete(T obj);
    }
}