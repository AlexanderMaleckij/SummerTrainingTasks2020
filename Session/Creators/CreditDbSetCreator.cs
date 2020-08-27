using ORM;
using Session.Models;
using System.Data;
using System.Data.SqlClient;

namespace Session.Creators
{
    class CreditDbSetCreator : DBSet<Credit>
    {
        IRepository<Credit> repository;

        public CreditDbSetCreator(DataSet ds, SqlConnection conn) : base(ds, conn)
        {
            LoadDbSet();
            repository = new Repository<Credit>(ds);
        }

        public override IRepository<Credit> FactoryMethod() => repository;
    }
}
