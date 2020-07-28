using Serialization.Serializators;

namespace Serialization.Serializers
{
    public class XmlSerializer<T> : Serializer<T>
    {
        public XmlSerializer(string fileName)
        {
            FileName = fileName;
        }
        
        internal override ISerialize<T> FactoryMethod()
        {
            return new XmlSerializator<T>(FileName);
        }
    }
}
