using System;
using System.Collections.Generic;
using System.Text;

namespace Serialization
{
    interface ISerialize<T>
    {
        public string FileName { get; set; }
        void SerializeItem(T item);
        T DeserializeItem();
        void SerializeCollection(ICollection<T> collection);
        ICollection<T> DeserializeCollection();
    }
}
