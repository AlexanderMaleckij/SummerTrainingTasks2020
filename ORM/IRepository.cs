using System.Collections;

namespace ORM
{
    public interface IRepository<T> : IEnumerable where T : ModelBase
    {
        void Add(T item);
        void Remove(T item);
        void Clear();
    }
}
