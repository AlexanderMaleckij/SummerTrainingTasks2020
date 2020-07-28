using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Student
{
    [Serializable]
    public class Student : IComparable<Student>
    {
        public string fullName;

        [XmlArray("StudentTests"), XmlArrayItem(typeof(TestMark), ElementName = "TestMark")]
        public List<TestMark> StudentTests { get; set; } = new List<TestMark>();

        public Student(string fullName)
        {
            this.fullName = fullName;
        }

        public Student() { }    //parameterless constructor for deserialization

        public int CompareTo(Student other) => fullName.CompareTo(other.fullName);

        public override bool Equals(object obj)
        {
            if(obj != null && obj is Student)
            {
                Student student = obj as Student;
                if(fullName == student.fullName && Enumerable.SequenceEqual(StudentTests, student.StudentTests))
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return fullName.GetHashCode() ^ StudentTests.GetHashCode();
        }
    }
}
