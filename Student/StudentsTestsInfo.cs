using System;
using System.Linq;
using System.Text;
using Tree;

namespace Student
{
    [Serializable]
    public class StudentsTestsInfo
    {
        public BinaryTree<Student> Students { get; set; } = new BinaryTree<Student>();

        public override string ToString()
        {
            StringBuilder testsInfo = new StringBuilder();

            foreach(Student student in Students)
            {
                testsInfo.AppendLine(student.ToString());
            }

            return testsInfo.ToString();
        }

        public override bool Equals(object obj)
        {
            if(obj != null && obj is StudentsTestsInfo)
            {
                StudentsTestsInfo testsInfo = obj as StudentsTestsInfo;

                if(Enumerable.SequenceEqual(Students, testsInfo.Students))
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Students.GetHashCode();
        }
    }
}
