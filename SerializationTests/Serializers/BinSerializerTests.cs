﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Serialization.Serializers.Tests
{
    [TestClass()]
    public class BinSerializerTests
    {
        [TestMethod()]
        public void BinSerializerTest()
        {
            List<string> stringsCollection = new List<string>() { "test", "strings", "for", "binary", "serialization" };

            Serializer<string> serializer = new BinSerializer<string>("serializationTest.bin");

            serializer.SerializeCollection(stringsCollection);
            Assert.IsTrue(Enumerable.SequenceEqual(stringsCollection, serializer.DeserializeCollection()));

            serializer.SerializeItem(stringsCollection[0]);
            Assert.AreEqual(stringsCollection[0], serializer.DeserializeItem());
        }
    }
}