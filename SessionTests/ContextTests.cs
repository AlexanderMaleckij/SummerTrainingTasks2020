using Microsoft.VisualStudio.TestTools.UnitTesting;
using Session;
using Session.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Session.Tests
{
    [TestClass()]
    public class ContextTests
    {
        string connectionString = string.Empty;
        private string SolutionPath
        {
            get => new Regex("^\\S+Task6\\\\").Match(typeof(ContextTests).Assembly.Location).Value;
        }
        private string ConnectionString
        {
            get
            {
                if(string.IsNullOrEmpty(connectionString))
                {
                    string dbPath = SolutionPath + @"Session\Database\Session.mdf";
                    return @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True";
                }

                return connectionString;
            }
        }

        [TestInitialize()]
        public void RestoreDBState()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string script = File.ReadAllText(SolutionPath + @"Session\Database\SqlSessionDBQuery.sql");
                new SqlCommand(script, connection).ExecuteNonQuery();
                connection.Close();
            }
            Context.DisposeInstance();
        }
        
        [TestMethod()]
        public void GetInstanceTest()
        {
            Assert.ThrowsException<ArgumentException>(() => Context.GetInstance()); //first call without connection string lead to ArgumentException
            Context context = Context.GetInstance(ConnectionString);
        }

        #region StudentGroup CRUD tests
        [TestMethod()]
        public void CreateStudentGroupTest()
        {
            Context.GetInstance(ConnectionString);

            Context.GetInstance().StudentGroups.Add(new StudentGroup("some group name", 2020));
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM StudentGroup WHERE  GroupName = @name", conn);
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = "some group name";
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Assert.AreEqual("some group name", (string)reader.GetValue(1));
                Assert.AreEqual(2020, (int)reader.GetValue(2));
                conn.Close();
            }
        }

        [TestMethod()]
        public void ReadStudentGroupTest()
        {
            Context.GetInstance(ConnectionString);
            List<StudentGroup> actual = Context.GetInstance().StudentGroups.ToList();
            List<StudentGroup> expected = new List<StudentGroup>()
            {
                new StudentGroup("IP-11", 2019),
                new StudentGroup("IS-11", 2019)
            };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UpdateStudentGroupTest()
        {
            Context.GetInstance(ConnectionString);

            Context.GetInstance().StudentGroups.Where(x => x.GroupName == "IP-11").First().GroupName = "some group name mod";
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM StudentGroup WHERE GroupName = @name", conn);
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = "some group name mod";
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Assert.AreEqual("some group name mod", (string)reader.GetValue(1));
                Assert.AreEqual(2019, (int)reader.GetValue(2));
                conn.Close();
            }
        }

        [TestMethod()]
        public void DeleteStudentGroupTest()
        {
            Context.GetInstance(ConnectionString);
            Context.GetInstance().StudentGroups.Add(new StudentGroup("some group name", 2020));
            Context.GetInstance().SaveChanges();
            Context.GetInstance().StudentGroups.Remove(Context.GetInstance().StudentGroups.Where(x => x.GroupName == "some group name").First());
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM StudentGroup WHERE GroupName = @name", conn);
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = "some group name";
                Assert.AreEqual(0, (int)command.ExecuteScalar());
                conn.Close();
            }
        }
        #endregion

        #region Student CRUD tests
        [TestMethod()]
        public void CreateStudentTest()
        {
            Context.GetInstance(ConnectionString);
            Context.GetInstance().Students.Add(new Student("Test Student", 1, 'm', new DateTime(2000, 12, 29)));
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Student WHERE  " +
                    "StudentGroupId = @groupId AND " +
                    "FullName = @fullName AND " +
                    "Gender = @gender AND " +
                    "DateOfBirth = @dateOfBirth", conn);
                command.Parameters.Add("@groupId",  SqlDbType.Int).Value = 1;
                command.Parameters.Add("@fullName", SqlDbType.NVarChar).Value = "Test Student";
                command.Parameters.Add("@gender",   SqlDbType.NVarChar).Value = 'm';
                command.Parameters.Add("@dateOfBirth", SqlDbType.DateTime).Value = new DateTime(2000, 12, 29);

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Assert.AreEqual(1, (int)reader.GetValue(1));
                Assert.AreEqual("Test Student", (string)reader.GetValue(2));
                Assert.AreEqual('m', ((string)reader.GetValue(3)).First());
                Assert.AreEqual(new DateTime(2000, 12, 29), (DateTime)reader.GetValue(4));
                conn.Close();
            }
        }

        [TestMethod()]
        public void ReadStudentTest()
        {
            Context.GetInstance(ConnectionString);
            List<Student> actual = Context.GetInstance().Students.ToList();
            List<Student> expected = new List<Student>()
            {
                new Student("Patrick Black",  1, 'm', new DateTime(2000, 02, 03)),
                new Student("Charles Backer", 1, 'm', new DateTime(2000, 01, 07)),
                new Student("Frank Thomas",   1, 'm', new DateTime(2001, 03, 05)),
                new Student("Alisha Edwards", 2, 'f', new DateTime(2000, 06, 05)),
                new Student("Leticia Ayrton", 2, 'f', new DateTime(2001, 03, 05)),
                new Student("Megan Gilson",   2, 'f', new DateTime(2000, 11, 11))
            };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UpdateStudentTest()
        {
            Student student = Context.GetInstance(ConnectionString).Students.Where(x => x.FullName == "Frank Thomas").First();
            student.FullName = "Test Name";
            student.Gender = 'f';
            student.DateOfBirth = new DateTime(2000, 11, 11);
            student.StudentGroupId = 2;
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Student WHERE " +
                    "StudentGroupId = @groupId AND " +
                    "FullName = @fullName AND " +
                    "Gender = @gender AND " +
                    "DateOfBirth = @dateOfBirth", conn);
                command.Parameters.Add("@groupId",  SqlDbType.Int).Value = 2;
                command.Parameters.Add("@fullName", SqlDbType.NVarChar).Value = "Test Name";
                command.Parameters.Add("@gender",   SqlDbType.NVarChar).Value = 'f';
                command.Parameters.Add("@dateOfBirth", SqlDbType.DateTime).Value = new DateTime(2000, 11, 11);

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Assert.AreEqual(2, (int)reader.GetValue(1));
                Assert.AreEqual("Test Name", (string)reader.GetValue(2));
                Assert.AreEqual('f', ((string)reader.GetValue(3)).First());
                Assert.AreEqual(new DateTime(2000, 11, 11), (DateTime)reader.GetValue(4));
                conn.Close();
            }
        }

        [TestMethod()]
        public void DeleteStudentTest()
        {
            Context.GetInstance(ConnectionString);
            Context.GetInstance().Students.Add(new Student("Test Student", 1, 'm', new DateTime(2000, 12, 29)));
            Context.GetInstance().SaveChanges();
            Context.GetInstance().Students.Remove(
                Context.GetInstance().Students.Where(x => 
                    x.FullName == "Test Student" && 
                    x.StudentGroupId == 1 && 
                    x.Gender == 'm' && 
                    x.DateOfBirth == new DateTime(2000, 12, 29)).First());
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Student WHERE " +
                   "StudentGroupId = @groupId AND " +
                   "FullName = @fullName AND " +
                   "Gender = @gender AND " +
                   "DateOfBirth = @dateOfBirth", conn);
                command.Parameters.Add("@groupId", SqlDbType.Int).Value = 1;
                command.Parameters.Add("@fullName", SqlDbType.NVarChar).Value = "Test Student";
                command.Parameters.Add("@gender", SqlDbType.NVarChar).Value = 'm';
                command.Parameters.Add("@dateOfBirth", SqlDbType.DateTime).Value = new DateTime(2000, 12, 29);
                Assert.AreEqual(0, (int)command.ExecuteScalar());
                conn.Close();
            }
        }
        #endregion

        #region KnowledgeControl CRUD tests
        [TestMethod()]
        public void CreateKnowledgeControlTest()
        {
            Context.GetInstance(ConnectionString);
            Context.GetInstance().KnowledgeControls.Add(new KnowledgeControl(1, "some subject", new DateTime(2019, 01, 05), 1));
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM KnowledgeControl WHERE " +
                    "Semester = @semester AND " +
                    "SubjectName = @subjectName AND " +
                    "PassDate = @passDate AND " +
                    "StudentGroupId = @studentGroupId", conn);
                command.Parameters.Add("@semester", SqlDbType.Int).Value = 1;
                command.Parameters.Add("@subjectName", SqlDbType.NVarChar).Value = "some subject";
                command.Parameters.Add("@passDate", SqlDbType.DateTime).Value = new DateTime(2019, 01, 05);
                command.Parameters.Add("@studentGroupId", SqlDbType.Int).Value = 1;

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Assert.AreEqual(1, (byte)reader.GetValue(1));
                Assert.AreEqual("some subject", (string)reader.GetValue(2));
                Assert.AreEqual(new DateTime(2019, 01, 05), (DateTime)reader.GetValue(3));
                Assert.AreEqual(1, (int)reader.GetValue(4));
                conn.Close();
            }
        }
        [TestMethod()]
        public void ReadKnowledgeControlTest()
        {
            Context.GetInstance(ConnectionString);
            List<KnowledgeControl> actual = Context.GetInstance().KnowledgeControls.ToList();
            List<KnowledgeControl> expected = new List<KnowledgeControl>()
            {
                new KnowledgeControl(1, "Philosophy", new DateTime(2018, 12, 24), 1),
                new KnowledgeControl(2, "English",    new DateTime(2018, 12, 26), 1),
                new KnowledgeControl(1, "OAIP",       new DateTime(2019, 01, 19), 1),
                new KnowledgeControl(2, "Math analysis", new DateTime(2019, 01, 24), 1),
                new KnowledgeControl(1, "Programming",   new DateTime(2018, 12, 26), 2),
                new KnowledgeControl(2, "OAIP",          new DateTime(2019, 01, 24), 2),
                new KnowledgeControl(1, "Discrete math", new DateTime(2018, 12, 24), 2),
                new KnowledgeControl(2, "Physical training", new DateTime(2019, 01, 19), 2)
            };
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void UpdateKnowledgeControlTest()
        {
            Context.GetInstance(ConnectionString);
            KnowledgeControl control = Context.GetInstance().KnowledgeControls.
                Where(x => 
                    x.StudentGroupId == 1 && 
                    x.SubjectName == "Philosophy" && 
                    x.PassDate == new DateTime(2018, 12, 24) && 
                    x.Semester == 1).First();
            control.StudentGroupId = 2;
            control.SubjectName = "New name";
            control.StudentGroupId = 2;
            control.PassDate = new DateTime(2018, 11, 25);
            control.Semester = 2;
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM KnowledgeControl WHERE " +
                   "Semester = @semester AND " +
                   "SubjectName = @subjectName AND " +
                   "PassDate = @passDate AND " +
                   "StudentGroupId = @studentGroupId", conn);
                command.Parameters.Add("@semester", SqlDbType.Int).Value = 2;
                command.Parameters.Add("@subjectName", SqlDbType.NVarChar).Value = "New name";
                command.Parameters.Add("@passDate", SqlDbType.DateTime).Value = new DateTime(2018, 11, 25);
                command.Parameters.Add("@studentGroupId", SqlDbType.Int).Value = 2;
                Assert.AreEqual(1, (int)command.ExecuteScalar());
                conn.Close();
            }
        }

        [TestMethod()]
        public void DeleteKnowledgeControlTest()
        {
            Context.GetInstance(ConnectionString);
            Context.GetInstance().KnowledgeControls.Add(new KnowledgeControl(1, "test subject", new DateTime(2018, 12, 24), 1));
            Context.GetInstance().SaveChanges();
            Context.GetInstance().KnowledgeControls.Remove(
                Context.GetInstance().KnowledgeControls.
                Where(x => 
                    x.StudentGroupId == 1 &&
                    x.SubjectName == "test subject" &&
                    x.PassDate == new DateTime(2018, 12, 24) &&
                    x.Semester == 1).First());
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM KnowledgeControl WHERE " +
                   "Semester = @semester AND " +
                   "SubjectName = @subjectName AND " +
                   "PassDate = @passDate AND " +
                   "StudentGroupId = @studentGroupId", conn);
                command.Parameters.Add("@semester", SqlDbType.Int).Value = 1;
                command.Parameters.Add("@subjectName", SqlDbType.NVarChar).Value = "test subject";
                command.Parameters.Add("@passDate", SqlDbType.DateTime).Value = new DateTime(2018, 12, 24);
                command.Parameters.Add("@studentGroupId", SqlDbType.Int).Value = 1;
                Assert.AreEqual(0, (int)command.ExecuteScalar());
                conn.Close();
            }
        }
        #endregion

        #region Exam CRUD tests
        [TestMethod()]
        public void CreateExamTest()
        {
            Context.GetInstance(ConnectionString);
            Context.GetInstance().Exams.Add(new Exam(8, 1, 10));
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Exam WHERE " +
                    "KnowledgeControlId = @knowledgeControlId AND " +
                    "StudentId = @studentId AND " +
                    "Mark = @mark", conn);
                command.Parameters.Add("@knowledgeControlId", SqlDbType.Int).Value = 8;
                command.Parameters.Add("@studentId", SqlDbType.Int).Value = 1;
                command.Parameters.Add("@mark", SqlDbType.TinyInt).Value = 10;

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Assert.AreEqual(8, (int)reader.GetValue(1));
                Assert.AreEqual(1, (int)reader.GetValue(2));
                Assert.AreEqual(10, (byte)reader.GetValue(3));
                conn.Close();
            }
        }

        [TestMethod()]
        public void ReadExamTest()
        {
            Context.GetInstance(ConnectionString);
            List<Exam> actual = Context.GetInstance().Exams.ToList();
            List<Exam> expected = new List<Exam>()
            {
                new Exam(3, 1, 8), new Exam(3, 2, 6), new Exam(3, 3, 3),
                new Exam(5, 1, 7), new Exam(5, 2, 4), new Exam(5, 3, 8),
                new Exam(4, 4, 9), new Exam(4, 5, 2), new Exam(4, 6, 8),
                new Exam(6, 4, 8), new Exam(6, 5, 3), new Exam(6, 6, 2)

            };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UpdateExamTest()
        {
            Context.GetInstance(ConnectionString);
            Exam exam = Context.GetInstance().Exams.Where(x => 
                x.KnowledgeControlId == 3 && 
                x.StudentId == 1 && 
                x.Mark == 8).First();
            exam.KnowledgeControlId = 2;
            exam.StudentId = 2;
            exam.Mark = 4;
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Exam WHERE " +
                    "KnowledgeControlId = @knowledgeControlId AND " +
                    "StudentId = @studentId AND " +
                    "Mark = @mark", conn);
                command.Parameters.Add("@knowledgeControlId", SqlDbType.Int).Value = 2;
                command.Parameters.Add("@studentId", SqlDbType.Int).Value = 2;
                command.Parameters.Add("@mark", SqlDbType.TinyInt).Value = 4;
                Assert.AreEqual(1, (int)command.ExecuteScalar());
                conn.Close();
            }
        }

        [TestMethod()]
        public void DeleteExamTest()
        {
            Exam examForRemove = Context.GetInstance(ConnectionString).Exams.First();
            Context.GetInstance().Exams.Remove(examForRemove);
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Exam WHERE " +
                     "KnowledgeControlId = @knowledgeControlId AND " +
                     "StudentId = @studentId AND " +
                     "Mark = @mark", conn);
                command.Parameters.Add("@knowledgeControlId", SqlDbType.Int).Value = examForRemove.KnowledgeControlId;
                command.Parameters.Add("@studentId", SqlDbType.Int).Value = examForRemove.StudentId;
                command.Parameters.Add("@mark", SqlDbType.TinyInt).Value = examForRemove.Mark;
                Assert.AreEqual(0, (int)command.ExecuteScalar());
                conn.Close();
            }
        }
        #endregion

        #region Credit CRUD tests
        [TestMethod()]
        public void CreateCreditTest()
        {
            Context.GetInstance(ConnectionString);
            Context.GetInstance().Credits.Add(new Credit(3, 1, true));
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Credit WHERE " +
                    "KnowledgeControlId  = @knowledgeControlId AND " +
                    "StudentId = @studentId AND " +
                    "IsPassed = @isPassed", conn);
                command.Parameters.Add("@knowledgeControlId", SqlDbType.Int).Value = 3;
                command.Parameters.Add("@studentId", SqlDbType.Int).Value = 1;
                command.Parameters.Add("@isPassed", SqlDbType.Bit).Value = true;
                Assert.AreEqual(1, (int)command.ExecuteScalar());
                conn.Close();
            }
        }

        [TestMethod()]
        public void ReadCreditTest()
        {
            Context.GetInstance(ConnectionString);
            List<Credit> actual = Context.GetInstance().Credits.ToList();
            List<Credit> expected = new List<Credit>()
            {
                new Credit(1, 1, true), new Credit(1, 2, true),  new Credit(1, 3, false),
                new Credit(7, 1, true), new Credit(7, 2, true),  new Credit(7, 3, true),
                new Credit(2, 4, true), new Credit(2, 5, false), new Credit(2, 6, false),
                new Credit(8, 4, true), new Credit(8, 5, false), new Credit(8, 6, true)
            };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UpdateCreditTest()
        {
            Context.GetInstance(ConnectionString);
            Credit credit = Context.GetInstance().Credits.Where(x =>
                x.KnowledgeControlId == 1 &&
                x.StudentId == 1 &&
                x.IsPassed == true).First();
            credit.KnowledgeControlId = 2;
            credit.StudentId = 2;
            credit.IsPassed = false;
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Credit WHERE " +
                    "KnowledgeControlId = @knowledgeControlId AND " +
                    "StudentId = @studentId AND " +
                    "IsPassed = @isPassed", conn);
                command.Parameters.Add("@knowledgeControlId", SqlDbType.Int).Value = 2;
                command.Parameters.Add("@studentId", SqlDbType.Int).Value = 2;
                command.Parameters.Add("@isPassed", SqlDbType.Bit).Value = false;
                Assert.AreEqual(1, (int)command.ExecuteScalar());
                conn.Close();
            }
        }

        [TestMethod()]
        public void DeleteCreditTest()
        {
            Credit creditForRemove = Context.GetInstance(ConnectionString).Credits.First();
            Context.GetInstance().Credits.Remove(creditForRemove);
            Context.GetInstance().SaveChanges();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Credit WHERE " +
                     "KnowledgeControlId = @knowledgeControlId AND " +
                     "StudentId = @studentId AND " +
                     "IsPassed = @isPassed", conn);
                command.Parameters.Add("@knowledgeControlId", SqlDbType.Int).Value = creditForRemove.KnowledgeControlId;
                command.Parameters.Add("@studentId", SqlDbType.Int).Value = creditForRemove.StudentId;
                command.Parameters.Add("@isPassed", SqlDbType.TinyInt).Value = creditForRemove.IsPassed;
                Assert.AreEqual(0, (int)command.ExecuteScalar());
                conn.Close();
            }
        }
        #endregion
    }
}