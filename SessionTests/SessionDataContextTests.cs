using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Session.Tests
{
    [TestClass()]
    public class SessionDataContextTests
    {
        private string SolutionPath
        {
            get => new Regex("^\\S+Task7\\\\").Match(typeof(SessionDataContextTests).Assembly.Location).Value;
        }
        public string ConnectionString
        {
            get
            {
                string dbPath = SolutionPath + @"Session\Session.mdf";
                return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True";
            }
        }

        [TestInitialize()]
        public void RunDatabaseScript()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string script = File.ReadAllText(SolutionPath + @"Session\SessionDbScript.sql");
                new SqlCommand(script, connection).ExecuteNonQuery();
                connection.Close();
            }
        }

        [TestMethod()]
        public void SessionDataContextTest()
        {
            using (SessionDataContext context = new SessionDataContext(ConnectionString))
            {
                Assert.IsNotNull(context);
            }
        }

        #region Credits CRUD
        [TestMethod]
        public void CreateCreditTest()
        {
            Credits credit = new Credits();
            credit.IsPassed = false;
            credit.KnowledgeControlId = 3;

            using (SessionDataContext context = new SessionDataContext(ConnectionString))
            {
                context.Credits.InsertOnSubmit(credit);
                context.SubmitChanges();
            }
            using (SessionDataContext context = new SessionDataContext(ConnectionString))
            {
                Assert.IsTrue(context.Credits.Contains(credit));
            }
        }

        [TestMethod]
        public void ReadCreditsTest()
        {
            List<Credits> actualCredits;
            using (SessionDataContext context = new SessionDataContext(ConnectionString))
            {
                actualCredits = context.Credits.ToList();
            }
            List<Credits> expectedCredits = new List<Credits>() { 
                new Credits(1, 1, true), new Credits(1, 2, true),  new Credits(1, 3, false),
                new Credits(7, 1, true), new Credits(7, 2, true),  new Credits(7, 3, true),
                new Credits(2, 4, true), new Credits(2, 5, false), new Credits(2, 6, false),
                new Credits(8, 4, true), new Credits(8, 5, false), new Credits(8, 6, true)};
            CollectionAssert.AreEqual(expectedCredits, actualCredits);
        }

        [TestMethod]
        public void UpdateCreditTest()
        {
            using (SessionDataContext context = new SessionDataContext(ConnectionString))
            {
                context.Credits.Where(x => x.Id == 1).First().IsPassed = false; //in database property has true value
                context.SubmitChanges();
            }
            using (SessionDataContext context = new SessionDataContext(ConnectionString))
            {
                Assert.AreEqual(false, context.Credits.Where(x => x.Id == 1).First().IsPassed);
            }
        }

        [TestMethod]
        public void DeleteCreditTest()
        {
            using (SessionDataContext context = new SessionDataContext(ConnectionString))
            {
                context.Credits.DeleteOnSubmit(context.Credits.Where(x => x.Id == 1).First());
                context.SubmitChanges();
            }
            using (SessionDataContext context = new SessionDataContext(ConnectionString))
            {
                Assert.IsFalse(context.Credits.Any(x => x.Id == 1));
            }
        }
        #endregion

        //other CRUD methods of the model classes should also 
        //work because they were also automatically generated
    }
}