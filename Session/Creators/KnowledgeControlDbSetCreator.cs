using ORM;
using Session.Models;
using System.Data;
using System.Data.SqlClient;

namespace Session.Creators
{
    class KnowledgeControlDbSetCreator : DBSet<KnowledgeControl>
    {
        IRepository<KnowledgeControl> repository;

        public KnowledgeControlDbSetCreator(DataSet ds, SqlConnection conn) : base(ds, conn)
        {
            LoadDbSet();
            repository = new Repository<KnowledgeControl>(ds);
        }

        public override IRepository<KnowledgeControl> FactoryMethod() => repository;
    }
}
