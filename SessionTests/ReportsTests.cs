using Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Session.Tests
{
    [TestClass()]
    public class ReportsTests
    {
        readonly SessionDataContext context = new SessionDataContext(new SessionDataContextTests().ConnectionString);

        [AssemblyInitialize()]
        public static void Init(TestContext context)
        {
            new SessionDataContextTests().RunDatabaseScript();
        }

        [TestMethod()]
        public void ReportsTest()
        {
            Reports reports = new Reports(context);
            Assert.IsNotNull(reports);
        }

        [TestMethod()]
        public void ReportLastSessionAllGroupsTest()
        {
            Reports reports = new Reports(context);
            var reportAllByAscending = reports.ReportLastSessionAllGroups(Sort.AscendingOrder, Sort.AscendingOrder, Sort.AscendingOrder);
            var reportAllByDescending = reports.ReportLastSessionAllGroups(Sort.DescendingOrder, Sort.DescendingOrder, Sort.DescendingOrder);
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string report1PathActual = directory + @"/ReportLastSessionAllGroupsAllByAscendingActual.xlsx";
            string report2PathActual = directory + @"/ReportLastSessionAllGroupsAllByDescendingActual.xlsx";
            string report1PathExpected = directory.Substring(0, directory.Length - 9) + "ReportLastSessionAllGroupsAllByAscendingExpected.xlsx";
            string report2PathExpected = directory.Substring(0, directory.Length - 9) + "ReportLastSessionAllGroupsAllByDescendingExpected.xlsx";
            if (File.Exists(report1PathActual))
                File.Delete(report1PathActual);
            if (File.Exists(report2PathActual))
                File.Delete(report2PathActual);
            reportAllByAscending.Save(report1PathActual);
            reportAllByDescending.Save(report2PathActual);
            reportAllByAscending.Dispose();
            reportAllByDescending.Dispose();
            ExcelAssert.ReportsTextSame(report1PathActual, report1PathExpected);
            ExcelAssert.ReportsTextSame(report2PathActual, report2PathExpected);
        }

        [TestMethod()]
        public void ReportPivotTablesEachSessionEachGroupTest()
        {
            Reports reports = new Reports(context);
            var reportAllByAscending = reports.ReportPivotTablesEachSessionEachGroup(Sort.AscendingOrder, Sort.AscendingOrder, Sort.AscendingOrder);
            var reportAllByDescending = reports.ReportPivotTablesEachSessionEachGroup(Sort.DescendingOrder, Sort.DescendingOrder, Sort.DescendingOrder);
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string report1PathActual = directory + @"/ReportPivotTablesEachSessionEachGroupAllByAscendingActual.xlsx";
            string report2PathActual = directory + @"/ReportPivotTablesEachSessionEachGroupAllByDescendingActual.xlsx";
            string report1PathExpected = directory.Substring(0, directory.Length - 9) + "ReportPivotTablesEachSessionEachGroupAllByAscendingExpected.xlsx";
            string report2PathExpected = directory.Substring(0, directory.Length - 9) + "ReportPivotTablesEachSessionEachGroupAllByDescendingExpected.xlsx";
            if (File.Exists(report1PathActual))
                File.Delete(report1PathActual);
            if (File.Exists(report2PathActual))
                File.Delete(report2PathActual);
            reportAllByAscending.Save(report1PathActual);
            reportAllByDescending.Save(report2PathActual);
            reportAllByAscending.Dispose();
            reportAllByDescending.Dispose();
            ExcelAssert.ReportsTextSame(report1PathActual, report1PathExpected);
            ExcelAssert.ReportsTextSame(report2PathActual, report2PathExpected);
        }

        [TestMethod()]
        public void DynamicSubjectsMarksChangeByYearsTest()
        {
            //! an empty cell means that there was no subject in this semester
            Reports reports = new Reports(context);
            var reportAllByAscending = reports.DynamicSubjectsMarksChangeByYears(Sort.AscendingOrder, Sort.AscendingOrder);
            var reportAllByDescending = reports.DynamicSubjectsMarksChangeByYears(Sort.DescendingOrder, Sort.DescendingOrder);
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string report1PathActual = directory + @"/DynamicSubjectsMarksChangeByYearsAllByAscendingActual.xlsx";
            string report2PathActual = directory + @"/DynamicSubjectsMarksChangeByYearsAllByDescendingActual.xlsx";
            string report1PathExpected = directory.Substring(0, directory.Length - 9) + "DynamicSubjectsMarksChangeByYearsAllByAscendingExpected.xlsx";
            string report2PathExpected = directory.Substring(0, directory.Length - 9) + "DynamicSubjectsMarksChangeByYearsAllByDescendingExpected.xlsx";
            if (File.Exists(report1PathActual))
                File.Delete(report1PathActual);
            if (File.Exists(report2PathActual))
                File.Delete(report2PathActual);
            reportAllByAscending.Save(report1PathActual);
            reportAllByDescending.Save(report2PathActual);
            reportAllByAscending.Dispose();
            reportAllByDescending.Dispose();
            ExcelAssert.ReportsTextSame(report1PathActual, report1PathExpected);
            ExcelAssert.ReportsTextSame(report2PathActual, report2PathExpected);
        }

        [TestMethod()]
        public void GetExpelledStudentsTest()
        {
            Reports reports = new Reports(context);
            List<int> expelledStudentsIP11Ids = reports.GetExpelledStudents(
                context.StudentGroups.Where(x => x.GroupName == "IP-11").First()).
                Select(x => x.Id).ToList();
            List<int> expelledStudentsIS11Ids = reports.GetExpelledStudents(
                 context.StudentGroups.Where(x => x.GroupName == "IS-11").First()).
                 Select(x => x.Id).ToList();
            CollectionAssert.AreEqual(new List<int> { 3 }, expelledStudentsIP11Ids);
            CollectionAssert.AreEqual(new List<int> { 5, 6 }, expelledStudentsIS11Ids);
        }
    }
}