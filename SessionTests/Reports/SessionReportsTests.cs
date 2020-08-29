using Excel;
using GroupDocs.Comparison;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Session.Tests;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;

namespace Session.Reports.Tests
{
    [TestClass()]
    public class SessionReportsTests
    {
        [AssemblyInitialize]
        public static void RestoreDbState(TestContext context) 
        {
            new ContextTests().RestoreDBState();
            Context.GetInstance(new ContextTests().ConnectionString);
        }

        [TestMethod()]
        public void ReportLastSessionAllGroupsTest()
        {
            SessionReports reports = new SessionReports();
            ExcelReport report = reports.ReportLastSessionAllGroups();
            report.IsVisible = true;
            string savePath = AppDomain.CurrentDomain.BaseDirectory + @"rez1.xlsx";
            if (File.Exists(savePath))
                File.Delete(savePath);
            report.Save(savePath);
            report.Dispose();
            Thread.Sleep(10);
            using (Comparer comparer = new Comparer(savePath))
            {
                comparer.Add("ref1.xlsx");
                comparer.Compare("test1 COMPARSION RESUT.xlsx");
            }
        }

        [TestMethod()]
        public void ReportPivotTablesEachTermEachGroupTest()
        {
            SessionReports reports = new SessionReports();
            ExcelReport report = reports.ReportPivotTablesEachTermEachGroup();
            report.IsVisible = true;
            string savePath = AppDomain.CurrentDomain.BaseDirectory + @"rez2.xlsx";
            if (File.Exists(savePath))
                File.Delete(savePath);
            report.Save(savePath);
            report.Dispose();
            Thread.Sleep(10);
            using (Comparer comparer = new Comparer(savePath))
            {
                comparer.Add("ref2.xlsx");
                comparer.Compare("test2 COMPARSION RESUT.xlsx");
            }
        }

        [TestMethod()]
        public void GetExpelledStudentsTest()
        {
            List<int> expelledStudentsIP11Ids = new SessionReports().
                GetExpelledStudents(
                    Context.GetInstance().
                    StudentGroups.
                    Where(x => x.GroupName == "IP-11").First()
                ).Select(x => x.Id).ToList();
            List<int> expelledStudentsIS11Ids = new SessionReports().
                GetExpelledStudents(
                    Context.GetInstance().
                    StudentGroups.
                    Where(x => x.GroupName == "IS-11").First()
                ).Select(x => x.Id).ToList();
            CollectionAssert.AreEqual(new List<int> {3 }, expelledStudentsIP11Ids);
            CollectionAssert.AreEqual(new List<int> {5, 6 }, expelledStudentsIS11Ids);
        }
    }
}