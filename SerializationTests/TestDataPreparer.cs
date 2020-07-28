using Student;
using System;
using System.Collections.Generic;

namespace SerializationTests
{
    class TestDataPreparer
    {
        public static StudentsTestsInfo StudentsTestsInfo
        {
            get
            {
                Test task1 = new Test("Declaring and calling methods", DateTime.Parse("17.07.2020"));
                Test task2 = new Test("Operations overload", DateTime.Parse("24.07.2020"));
                Student.Student student0 = new Student.Student("Alexander Maletski");
                Student.Student student1 = new Student.Student("Egor Usachev");
                Student.Student student2 = new Student.Student("Ilya Hruschev");
                student0.StudentTests.Add(new TestMark(task1, 8));
                student0.StudentTests.Add(new TestMark(task2, 9));
                student1.StudentTests.Add(new TestMark(task1, 8));
                student1.StudentTests.Add(new TestMark(task2, 7));
                student2.StudentTests.Add(new TestMark(task1, 8));
                student2.StudentTests.Add(new TestMark(task2, 8));

                StudentsTestsInfo studentsTestsInfo = new StudentsTestsInfo();
                studentsTestsInfo.Students.AddRange(new List<Student.Student>() { student0, student1, student2 });

                return studentsTestsInfo;
            }
        }
    }
}
