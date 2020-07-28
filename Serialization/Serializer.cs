using System.Collections.Generic;

namespace Serialization
{
    public abstract class Serializer<T> : ISerialize<T>
    {
        public string FileName { get; set; }

        internal abstract ISerialize<T> FactoryMethod();

        public ICollection<T> DeserializeCollection()
        {
            var serializer = FactoryMethod();
            return serializer.DeserializeCollection();
        }

        public T DeserializeItem()
        {
            var serializer = FactoryMethod();
            return serializer.DeserializeItem();
        }

        public void SerializeCollection(ICollection<T> collection)
        {
            var serializer = FactoryMethod();
            serializer.SerializeCollection(collection);
        }

        public void SerializeItem(T item)
        {
            var serializer = FactoryMethod();
            serializer.SerializeItem(item);
        }
    }
}
