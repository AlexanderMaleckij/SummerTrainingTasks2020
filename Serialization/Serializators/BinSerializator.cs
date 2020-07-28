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
                deserializedCollection = (ICollection<T>)formatter.Deserialize(fs);
            }

            return deserializedCollection;
        }

        public T DeserializeItem()
        {
            T deserializedItem = default;
            BinaryFormatter formatter = new BinaryFormatter();
            using(FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                deserializedItem = (T)formatter.Deserialize(fs);
            }

            return deserializedItem;
        }

        public void SerializeCollection(ICollection<T> collection)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, collection);
                fs.Flush();
            }
        }

        public void SerializeItem(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, item);
                fs.Flush();
            }
        }
    }
}
