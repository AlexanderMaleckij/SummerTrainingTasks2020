using System;
using System.Collections.Generic;

namespace Student
{
    public class Student : IComparable<Student>
    {
        public readonly string fullName;
        public List<TestMark> StudentTests { get; set; } = new List<TestMark>();

        public Student(string fullName)
        {
            this.fullName = fullName;
        }

        public int CompareTo(Student other) => fullName.CompareTo(other.fullName);
    }
}
