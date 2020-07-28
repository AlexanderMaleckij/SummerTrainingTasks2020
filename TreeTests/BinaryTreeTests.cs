using Microsoft.VisualStudio.TestTools.UnitTesting;
using Student;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tree.Tests
{
    [TestClass()]
    public class BinaryTreeTests
    {
        List<Student.Student> GetStudents
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

                return new List<Student.Student>() { student0, student1, student2 };
            }
        }

        [TestMethod()]
        public void AddTest()
        {
            List<Student.Student> studentsCollection = GetStudents;

            StudentsTestsInfo students = new StudentsTestsInfo();   //contains  BinaryTree<Student> Students (auto property)

            Assert.IsFalse(students.Students.Contains(studentsCollection[0]));
            Assert.IsFalse(students.Students.Contains(studentsCollection[1]));
            Assert.IsFalse(students.Students.Contains(studentsCollection[2]));

            students.Students.Add(studentsCollection[0]);
            students.Students.Add(studentsCollection[1]);
            students.Students.Add(studentsCollection[2]);

            Assert.IsTrue(students.Students.Contains(studentsCollection[0]));
            Assert.IsTrue(students.Students.Contains(studentsCollection[1]));
            Assert.IsTrue(students.Students.Contains(studentsCollection[2]));

            var ex = Assert.ThrowsException<BinaryTreeException>(() => students.Students.Add(studentsCollection[0]));
            Assert.AreEqual(ex.Message, "Value with same key also exists in the tree");
        }

        [TestMethod()]
        public void AddRangeTest()
        {
            List<Student.Student> studentsCollection = GetStudents;

            StudentsTestsInfo students = new StudentsTestsInfo();

            Assert.IsFalse(students.Students.Contains(studentsCollection[0]));
            Assert.IsFalse(students.Students.Contains(studentsCollection[1]));
            Assert.IsFalse(students.Students.Contains(studentsCollection[2]));

            students.Students.AddRange(studentsCollection);

            Assert.IsTrue(students.Students.Contains(studentsCollection[0]));
            Assert.IsTrue(students.Students.Contains(studentsCollection[1]));
            Assert.IsTrue(students.Students.Contains(studentsCollection[2]));

            var ex = Assert.ThrowsException<BinaryTreeException>(() => students.Students.Add(studentsCollection[0]));
            Assert.AreEqual(ex.Message, "Value with same key also exists in the tree");
        }

        [TestMethod()]
        public void RemoveTest()
        {
            List<Student.Student> studentsCollection = GetStudents;

            StudentsTestsInfo students = new StudentsTestsInfo();

            Assert.IsFalse(students.Students.Contains(studentsCollection[0]));
            Assert.IsFalse(students.Students.Contains(studentsCollection[1]));
            Assert.IsFalse(students.Students.Contains(studentsCollection[2]));

            students.Students.AddRange(studentsCollection);

            students.Students.Remove(studentsCollection[0]);
            Assert.IsFalse(students.Students.Contains(studentsCollection[0]));
            Assert.IsTrue(students.Students.Contains(studentsCollection[1]));
            Assert.IsTrue(students.Students.Contains(studentsCollection[2]));

            students.Students.Remove(studentsCollection[1]);
            Assert.IsFalse(students.Students.Contains(studentsCollection[0]));
            Assert.IsFalse(students.Students.Contains(studentsCollection[1]));
            Assert.IsTrue(students.Students.Contains(studentsCollection[2]));

            students.Students.Remove(studentsCollection[2]);
            Assert.IsFalse(students.Students.Contains(studentsCollection[0]));
            Assert.IsFalse(students.Students.Contains(studentsCollection[1]));
            Assert.IsFalse(students.Students.Contains(studentsCollection[2]));
        }

        [TestMethod()]
        public void ClearTest()
        {
            List<Student.Student> studentsCollection = GetStudents;
            StudentsTestsInfo students = new StudentsTestsInfo();
            students.Students.AddRange(studentsCollection);

            Assert.IsTrue(students.Students.Contains(studentsCollection[0]));
            Assert.IsTrue(students.Students.Contains(studentsCollection[1]));
            Assert.IsTrue(students.Students.Contains(studentsCollection[2]));

            students.Students.Clear();

            Assert.IsFalse(students.Students.Contains(studentsCollection[0]));
            Assert.IsFalse(students.Students.Contains(studentsCollection[1]));
            Assert.IsFalse(students.Students.Contains(studentsCollection[2]));
        }

        [TestMethod()]
        public void MinTest()
        {
            List<Student.Student> studentsCollection = GetStudents;
            StudentsTestsInfo students = new StudentsTestsInfo();
            students.Students.AddRange(studentsCollection);

            Assert.AreEqual(studentsCollection[0], students.Students.Min());    //because of Student class realize Icomparable by FullName field
        }

        [TestMethod()]
        public void MaxTest()
        {
            List<Student.Student> studentsCollection = GetStudents;
            StudentsTestsInfo students = new StudentsTestsInfo();
            students.Students.AddRange(studentsCollection);

            Assert.AreEqual(studentsCollection[2], students.Students.Max());    //because of Student class realize Icomparable by FullName field
        }

        [TestMethod()]
        public void CopyToTest()
        {
            BinaryTree<int> intTree = new BinaryTree<int>();
            intTree.AddRange(Enumerable.Range(0, 5));

            int[] array = new int[5];
            intTree.CopyTo(array, 0);

            Assert.IsTrue(Enumerable.SequenceEqual(Enumerable.Range(0, 5), array));
        }

        [TestMethod()]
        public void EqualsTest()
        {
            BinaryTree<int> tree1 = new BinaryTree<int>();
            tree1.AddRange(Enumerable.Range(0, 10));

            BinaryTree<int> tree2 = new BinaryTree<int>();
            tree2.AddRange(Enumerable.Range(0, 10));

            BinaryTree<int> tree3 = new BinaryTree<int>();
            tree3.AddRange(Enumerable.Range(0, 11));

            Assert.AreEqual(tree1, tree2);
            Assert.AreNotEqual(tree1, tree3);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.AddRange(Enumerable.Range(0, 10));
            //expected balanced tree in string
            string treeStr = "               9\n" +
                             "          8\n"      +
                             "     7\n"           +
                             "               6\n" +
                             "          5\n"      +
                             "               4\n" +
            /*root -->*/     "3\n"                +
                             "          2\n"      +
                             "     1\n"           +
                             "          0\n";

            Assert.AreEqual(tree.ToString(), treeStr);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            BinaryTree<int> tree1 = new BinaryTree<int>();
            tree1.AddRange(Enumerable.Range(1, 10));

            BinaryTree<int> tree2 = new BinaryTree<int>();
            tree2.AddRange(Enumerable.Range(1, 100));

            Assert.AreNotEqual(tree1.GetHashCode(), tree2.GetHashCode());
        }
    }
}