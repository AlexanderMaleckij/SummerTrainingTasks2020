using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Serialization.Serializers.Tests
{
    [TestClass()]
    public class JsonSerializerTests
    {
        [TestMethod()]
        public void JsonSerializerTest()
        {
            List<string> stringsCollection = new List<string>() { "test", "strings", "for", "json", "serialization" };

            Serializer<string> serializer = new JsonSerializer<string>("serializationTest.json");

            serializer.SerializeCollection(stringsCollection);
            Assert.IsTrue(Enumerable.SequenceEqual(stringsCollection, serializer.DeserializeCollection()));

            serializer.SerializeItem(stringsCollection[0]);
            Assert.AreEqual(stringsCollection[0], serializer.DeserializeItem());
        }
    }
}