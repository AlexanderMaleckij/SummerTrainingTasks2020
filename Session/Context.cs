using ORM;
using Session.Creators;
using Session.Models;
using System;
using System.Data.SqlClient;

namespace Session
{
    public class Context : ContextBase
    {
        public DBSet<Credit> Credits { get; set; }
        public DBSet<Exam> Exams { get; set; }
        public DBSet<StudentGroup> StudentGroups { get; set; }
        public DBSet<Student> Students { get; set; }
        public DBSet<KnowledgeControl> KnowledgeControls { get; set; }

        #region Singleton part
        private static Context contextInstance;
        private Context(string connectionString) : base(connectionString)
        {
            StudentGroups = new StudentsGroupDbSetCreator(DataSet, Connection);
            Students = new StudentDbSetCreator(DataSet, Connection);
            KnowledgeControls = new KnowledgeControlDbSetCreator(DataSet, Connection);
            Credits = new CreditDbSetCreator(DataSet, Connection);
            Exams = new ExamDbSetCreator(DataSet, Connection);
        }
        public static Context GetInstance(string connectionString = "")
        {
            if (contextInstance == null && connectionString == string.Empty)
            {
                throw new ArgumentException($"At the first call, the {nameof(connectionString)} parameter must be specified");
            }

            if (contextInstance == null)
            {
                contextInstance = new Context(connectionString);
                contextInstance.Connection = new SqlConnection(connectionString);
            }

            return contextInstance;
        }
        #endregion
        
        public static void DisposeInstance()
        {
            if(contextInstance != null)
            {
                contextInstance = null;
            }
        }

        public override void SaveChanges() => SaveChanges(this);
    }
}
