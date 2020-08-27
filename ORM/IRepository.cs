using System.Collections;

namespace ORM
{
    //Represents Product interface (in factory method pattern diagram)
    //https://refactoring.guru/images/patterns/diagrams/factory-method/structure.png


    public interface IRepository<T> : IEnumerable where T : ModelBase
    {
        void Add(T item);
        void Remove(T item);
        void Clear();
    }
}
