using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Serialization.Serializers.Tests
{
    [TestClass()]
    public class XmlSerializerTests
    {
        [TestMethod()]
        public void XmlSerializerTest()
        {
            ICollection<string> stringsCollection = new List<string>() { "test", "strings", "for", "xml", "serialization" };

            Serializer<string> serializer = new XmlSerializer<string>("serializationTest.xml");

            serializer.SerializeCollection(stringsCollection);
            Assert.IsTrue(Enumerable.SequenceEqual(stringsCollection, serializer.DeserializeCollection()));

            serializer.SerializeItem(stringsCollection.First());
            Assert.AreEqual(stringsCollection.First(), serializer.DeserializeItem());
        }
    }
}