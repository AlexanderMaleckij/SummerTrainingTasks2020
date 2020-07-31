using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Serialization.Serializators
{
    class JsonSerializator<T> : ISerialize<T>
    {
        public string FileName { get; set; }

        public JsonSerializator(string fileName)
        {
            FileName = fileName;
        }

        public ICollection<T> DeserializeCollection()
        {
            ICollection<T> deserializedCollection = default;
            using (StreamReader sr = new StreamReader(FileName))
            {
                var wrappedCollection = JsonSerializer.Deserialize<VersionWrapper<ICollection<T>>>(sr.ReadToEnd());

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
            using (StreamReader sr = new StreamReader(FileName))
            {
                var wrappedItem = JsonSerializer.Deserialize<VersionWrapper<T>>(sr.ReadToEnd());

                if (wrappedItem.IsClassChanged)
                {
                    throw new SerializationVersionException("the class has changed since the serialization of this file");
                }

                deserializedItem = wrappedItem.Content;
            }

            return deserializedItem;
        }

        public void SerializeCollection(ICollection<T> collection)
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                sw.Write(JsonSerializer.Serialize(new VersionWrapper<ICollection<T>>(collection)));
                sw.Flush();
            }
        }

        public void SerializeItem(T item)
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                sw.Write(JsonSerializer.Serialize(new VersionWrapper<T>(item)));
                sw.Flush();
            } 
        }
    }
}
