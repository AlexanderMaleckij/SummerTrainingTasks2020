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
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(VersionWrapper<List<T>>));
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                var wrappedCollection = (VersionWrapper<List<T>>)xmlSerializer.Deserialize(fs);

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
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(VersionWrapper<T>));
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                var wrappedItem = (VersionWrapper<T>)xmlSerializer.Deserialize(fs);

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
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(VersionWrapper<List<T>>));
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                xmlSerializer.Serialize(sw, new VersionWrapper<List<T>>((List<T>)collection));
                sw.Flush();
            }
        }

        public void SerializeItem(T item)
        {
            XmlSerializer xml = new XmlSerializer(typeof(VersionWrapper<T>));
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                xml.Serialize(sw, new VersionWrapper<T>(item));
                sw.Flush();
            }
        }
    }
}
