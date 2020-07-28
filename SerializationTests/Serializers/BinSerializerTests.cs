using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializationTests;
using Student;
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
            StudentsTestsInfo info = TestDataPreparer.StudentsTestsInfo;

            Serializer<Student.Student> serializer = new BinSerializer<Student.Student>("serializationTest.bin");

            serializer.SerializeCollection(info.Students);

            ICollection<Student.Student> actual = serializer.DeserializeCollection();

            Assert.IsTrue(Enumerable.SequenceEqual(info.Students, actual));
        }
    }
}