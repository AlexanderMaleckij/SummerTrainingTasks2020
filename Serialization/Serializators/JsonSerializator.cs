using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Serialization
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
                deserializedCollection = JsonSerializer.Deserialize<ICollection<T>>(sr.ReadToEnd());
            }

            return deserializedCollection;
        }

        public T DeserializeItem()
        {
            T deserializedItem = default;
            using (StreamReader sr = new StreamReader(FileName))
            {
                deserializedItem = JsonSerializer.Deserialize<T>(sr.ReadToEnd());
            }

            return deserializedItem;
        }

        public void SerializeCollection(ICollection<T> collection)
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                sw.Write(JsonSerializer.Serialize(collection));
                sw.Flush();
            }
        }

        public void SerializeItem(T item)
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                sw.Write(JsonSerializer.Serialize(item));
                sw.Flush();
            } 
        }
    }
}
