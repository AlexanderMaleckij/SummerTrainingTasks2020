using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ORM
{
    /// <summary>
    /// Base class for all contexts
    /// </summary>
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

        /// <summary>
        /// Method that causes changes to be saved to the database for each DbSet set
        /// </summary>
        /// <typeparam name="T">type of concrete context</typeparam>
        /// <param name="concreteContext">concrete context instance</param>
        protected static void SaveChanges<T>(T concreteContext) where T : ContextBase
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
