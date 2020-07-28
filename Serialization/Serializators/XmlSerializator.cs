using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Serialization.Serializators
{
    class XmlSerializator<T> : ISerialize<T>
    {
        public string FileName { get; set; }

        public XmlSerializator(string fileName)
        {
            FileName = fileName;
        }

        public ICollection<T> DeserializeCollection()
        {
            ICollection<T> deserializedCollection = default;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ICollection<T>));
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                deserializedCollection = (ICollection<T>)xmlSerializer.Deserialize(fs);
            }

            return deserializedCollection;
        }

        public T DeserializeItem()
        {
            T deserializedItem = default;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                deserializedItem = (T)xmlSerializer.Deserialize(fs);
            }

            return deserializedItem;
        }

        public void SerializeCollection(ICollection<T> collection)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ICollection<T>));
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                xmlSerializer.Serialize(sw, collection);
                sw.Flush();
            }
        }

        public void SerializeItem(T item)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                xml.Serialize(sw, item);
                sw.Flush();
            }
        }
    }
}
