using ORM;
using Session.Models;
using System.Data;
using System.Data.SqlClient;

namespace Session.Creators
{
    class StudentsGroupDbSetCreator : DBSet<StudentGroup>
    {
        IRepository<StudentGroup> repository;

        public StudentsGroupDbSetCreator(DataSet ds, SqlConnection conn) : base(ds, conn)
        {
            LoadDbSet();
            repository = new Repository<StudentGroup>(ds);
        }

        public override IRepository<StudentGroup> FactoryMethod() => repository;
    }
}
