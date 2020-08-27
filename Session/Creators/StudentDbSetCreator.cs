using ORM;
using Session.Models;
using System.Data;
using System.Data.SqlClient;

namespace Session.Creators
{
    class StudentDbSetCreator : DBSet<Student>
    {
        IRepository<Student> repository;

        public StudentDbSetCreator(DataSet ds, SqlConnection conn) : base(ds, conn)
        {
            LoadDbSet();
            repository = new Repository<Student>(ds);
        }

        public override IRepository<Student> FactoryMethod() => repository;
    }
}
