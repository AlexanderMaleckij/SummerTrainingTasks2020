using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization.Serializators
{
    class BinSerializator<T> : ISerialize<T>
    {
        public string FileName { get; set; }

        public BinSerializator(string fieName)
        {
            FileName = fieName;
        }

        public ICollection<T> DeserializeCollection()
        {
            ICollection<T> deserializedCollection = default;
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                var wrappedCollection = (VersionWrapper<ICollection<T>>)formatter.Deserialize(fs);

                if(wrappedCollection.IsClassChanged)
                {
                    throw new SerializationVersionException("the class has changed since the serialization of this file");
                }
                deserializedCollection = wrappedCollection.Content;
            }

            return deserializedCollection;
        }

        public T DeserializeItem()
        {
            T deserializedItem = default;
            BinaryFormatter formatter = new BinaryFormatter();
            using(FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                var wrappedItem = (VersionWrapper<T>)formatter.Deserialize(fs);

                if(wrappedItem.IsClassChanged)
                {
                    throw new SerializationVersionException("the class has changed since the serialization of this file");
                }
                deserializedItem = wrappedItem.Content;
            }

            return deserializedItem;
        }

        public void SerializeCollection(ICollection<T> collection)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, new VersionWrapper<ICollection<T>>(collection));
                fs.Flush();
            }
        }

        public void SerializeItem(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, new VersionWrapper<T>(item));
                fs.Flush();
            }
        }
    }
}
