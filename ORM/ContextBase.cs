using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ORM
{
    public abstract class ContextBase
    {
        protected DataSet DataSet { get; set; } = new DataSet();
        protected SqlConnection Connection { get; set; }

        public ContextBase(string connectionStr)
        {
            if(!string.IsNullOrEmpty(connectionStr))
            {
                Connection = new SqlConnection(connectionStr);
            }
        }

        public abstract void SaveChanges();

        public static void SaveChanges<T>(T concreteContext) where T : ContextBase
        {
            PropertyInfo[] pi = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var property in pi)
            {
                if (property.PropertyType.IsGenericType)
                {
                    object dbSet = property.GetValue(concreteContext);

                    if(dbSet is IUnitOfWork)
                    {
                        (dbSet as IUnitOfWork).Commit();
                    }
                }
            }
        }
    }
}
