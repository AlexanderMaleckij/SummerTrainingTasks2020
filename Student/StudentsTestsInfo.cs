using System.Text;
using Tree;

namespace Student
{
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
    }
}
