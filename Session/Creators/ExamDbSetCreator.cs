using ORM;
using Session.Models;
using System.Data;
using System.Data.SqlClient;

namespace Session.Creators
{
    class ExamDbSetCreator : DBSet<Exam>
    {
        IRepository<Exam> repository;

        public ExamDbSetCreator(DataSet ds, SqlConnection conn) : base(ds, conn)
        {
            LoadDbSet();
            repository = new Repository<Exam>(ds);
        }

        public override IRepository<Exam> FactoryMethod() => repository;
      
    }
}
