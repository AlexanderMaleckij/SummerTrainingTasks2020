﻿using Serialization.Serializators;

namespace Serialization.Serializers
{
    class JsonSerializer<T> : Serializer<T>
    {
        public JsonSerializer(string fileName)
        {
            FileName = fileName;
        }

        internal override ISerialize<T> FactoryMethod()
        {
            return new JsonSerializator<T>(FileName);
        }
    }
}
