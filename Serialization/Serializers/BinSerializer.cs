using Serialization.Serializators;

namespace Serialization.Serializers
{
    public class BinSerializer<T> : Serializer<T>
    {
        public BinSerializer(string fileName)
        {
            FileName = fileName;
        }

        internal override ISerialize<T> FactoryMethod()
        {
            return new BinSerializator<T>(FileName);
        }
    }
}
