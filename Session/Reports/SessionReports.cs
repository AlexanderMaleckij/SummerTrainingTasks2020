using Excel;
using Excel.Items;
using Excel.Properties;
using Excel.Containers;
using Session.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Data;

namespace Session.Reports
{
    public class SessionReports
    {
        Context context = Context.GetInstance();

        /// <summary>
        /// Prepare ExcelReport of session results 
        /// for each group in the form of a table
        /// </summary>
        /// <returns>report instance</returns>
        public ExcelReport ReportLastSessionAllGroups()
        {
            var creditsExams = new ExcelStackPanel(Orientation.Vertical);

            foreach(StudentGroup group in context.StudentGroups)
            {
                int latestSemester = LatestGroupSemester(group);

                var creditExam = new ExcelStackPanel(Orientation.Horizintal);
                creditExam.Items.Add(new ExcelTable(GetExamsTable(group, latestSemester)));
                creditExam.Items.Add(new ExcelTable(GetCreditsTable(group, latestSemester)));

                var header = new ExcelText($"Session results of the {group.GroupName} group");
                header.Size.Width = creditExam.Size.Width;
                header.Size.Height = 2;
                header.Styler.BackgroundColor = XlRgbColor.rgbDarkGray;
                header.Styler.FontColor =       XlRgbColor.rgbWhiteSmoke;
                header.Styler.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                header.Styler.VerticalAlignment =   XlVAlign.xlVAlignCenter;
                header.Styler.FontSize = 20;
                var creditExamWithTitle = new ExcelStackPanel(Orientation.Vertical);
                creditExamWithTitle.Items.Add(header);

                creditsExams.Items.Add(creditExamWithTitle);
            }

            var excelReport = new ExcelReport();
            excelReport.AddReportItem(creditsExams);
            return excelReport;
        }

        /// <summary>
        /// Form excel report for each session, display a pivot table 
        /// with the average / minimum / maximum mark for each group
        /// </summary>
        /// <returns>report instance</returns>
        public ExcelReport ReportPivotTablesEachTermEachGroup()
        {
            var stackPanel = new ExcelStackPanel(Orientation.Vertical); //main container
            stackPanel.SpaceBetween = 2;
            foreach (var academicYearGroups in context.StudentGroups.GroupBy(x => x.TransitionYear))
            {
                stackPanel.Items.Add(
                    CreateAcademicYearPanel(
                        academicYearGroups.Key,
                        GetPivotTable(academicYearGroups.ToList(), 1),
                        GetPivotTable(academicYearGroups.ToList(), 2)));
            }

            var report = new ExcelReport();
            report.AddReportItem(stackPanel);
            return report;
        }

        /// <summary>
        /// Prepare expelled students list of a given group
        /// </summary>
        /// <param name="group">students group</param>
        /// <returns>list of expelled students</returns>
        public List<Student> GetExpelledStudents(StudentGroup group)
        {
            List<Student> expelledStudents = new List<Student>();
            var groupTerms = context.KnowledgeControls.ToList().Where(x => x.StudentGroupId == group.Id).Select(x => x.Semester).ToList();
            foreach (int groupTerm in groupTerms)
            {
                var studFullNames = new PivotDataTable(GetExamsTable(group, groupTerm)).ExpelledStudents;
                expelledStudents.AddRange(context.Students.Where(x => studFullNames.Any(y => y == x.FullName)));
            }

            expelledStudents.Distinct();
            return expelledStudents;
        }

        /// <summary>
        /// Prepare a table table of students and their marks by each passed subject
        /// </summary>
        /// <param name="group">group</param>
        /// <param name="semester">semester (1-winter 2-summer)</param>
        /// <returns>table with passing results</returns>
        private DataTable GetExamsTable(StudentGroup group, int semester)
        {
            var students = context.Students.Where(x => x.StudentGroupId == group.Id);
            var examsControls = context.KnowledgeControls.GroupJoin(
                context.Exams,
                co => co.Id,
                ex => ex.KnowledgeControlId,
                (co, ex) => new
                {
                    co.SubjectName,
                    co.PassDate,
                    StudentResult = new 
                    { 
                        students.Where(x => x.Id == context.Exams.Where(t => t.KnowledgeControlId == co.Id).First().StudentId).First().FullName,
                        context.Exams.Where(x => x.KnowledgeControlId == co.Id).First().Mark
                    }
                });
            var dtCreator = new PivotDataTable(
                "Student name", 
                students.Select(x => x.FullName).ToList(),
                examsControls.Select(x => x.SubjectName).ToList());
            examsControls.ToList().ForEach(x => dtCreator[x.StudentResult.FullName, x.SubjectName] = x.StudentResult.Mark.ToString());
            return dtCreator.DataTable;
        }

        /// <summary>
        /// Prepare a table with the results of passing credits
        /// </summary>
        /// <param name="group">group</param>
        /// <param name="semester">semester</param>
        /// <returns>table with passing results</returns>
        private DataTable GetCreditsTable(StudentGroup group, int semester)
        {
            var students = context.Students.Where(x => x.StudentGroupId == group.Id);
            var creditsControls = context.KnowledgeControls.GroupJoin(
                context.Credits,
                co => co.Id,
                cr => cr.KnowledgeControlId,
                (co, cr) => new
                {
                    co.SubjectName,
                    co.PassDate,
                    StudentResult = new
                    {
                        students.Where(x => x.Id == context.Credits.Where(t => t.KnowledgeControlId == co.Id).First().StudentId).First().FullName,
                        context.Credits.Where(x => x.KnowledgeControlId == co.Id).First().IsPassed
                    }
                });
            var dtCreator = new PivotDataTable(
                "Student name",
                students.Select(x => x.FullName).ToList(),
                creditsControls.Select(x => x.SubjectName).ToList());
            creditsControls.ToList().ForEach(x => dtCreator[x.StudentResult.FullName, x.SubjectName] = x.StudentResult.IsPassed ? "Passed" : string.Empty);
            return dtCreator.DataTable;
        }

        /// <summary>
        /// Gets the group's past semester
        /// </summary>
        /// <param name="group">group</param>
        /// <returns>past semester number</returns>
        private int LatestGroupSemester(StudentGroup group)
        {
            return Context.GetInstance().KnowledgeControls.
                Where(x => x.StudentGroupId == group.Id).
                Select(x => x.Semester).
                Max();
        }

        /// <summary>
        /// Build a pivot table with the average / minimum /
        /// maximum mark for given groups in specified semester
        /// </summary>
        /// <param name="studentGroups">students groups</param>
        /// <param name="semester">semester number</param>
        /// <returns>pivot table</returns>
        private DataTable GetPivotTable(List<StudentGroup> studentGroups, int semester)
        {
            PivotDataTable dataTable = new PivotDataTable(
                "Mark",
                new List<string>() { "Average", "Minimal", "Maximal" },
                studentGroups.Select(x => x.GroupName).ToList());

            foreach(StudentGroup group in studentGroups)
            {
                var studentResults = new PivotDataTable(GetExamsTable(group, semester));
                dataTable["Average", group.GroupName] = studentResults.AverageMark.ToString();
                dataTable["Minimal", group.GroupName] = studentResults.MinMark.ToString();
                dataTable["Maximal", group.GroupName] = studentResults.MaxMark.ToString();
            }

            return dataTable.DataTable;
        }

        /// <summary>
        /// Create a tables with headers with header 
        /// for both tables in the StackPanel instance
        /// </summary>
        /// <param name="year">year of study</param>
        /// <param name="winter">table with winter term passing results</param>
        /// <param name="summer">table with summer term passing results</param>
        /// <returns>Stack panel containing both tables and named headers</returns>
        private ExcelStackPanel CreateAcademicYearPanel(int year, DataTable winter, DataTable summer)
        {
            var academicYearPanel = new ExcelStackPanel(Orientation.Vertical);
            var header = new ExcelText($"Session {year} - {year + 1} pivot table");
            StyleTitleText(header);
            academicYearPanel.Items.Add(header);
            var summerWinterTermsPanel = new ExcelStackPanel(Orientation.Horizintal);
            academicYearPanel.Items.Add(summerWinterTermsPanel);
            summerWinterTermsPanel.Items.Add(CreateSemesterPanel("Winter", winter));
            summerWinterTermsPanel.Items.Add(CreateSemesterPanel("Summer", summer));
            header.Size.Width = academicYearPanel.Size.Width;
            return academicYearPanel;
        }

        /// <summary>
        /// Creates a table with a title boxed in the stackPanel instance
        /// </summary>
        /// <param name="title">table title</param>
        /// <param name="table">table</param>
        /// <returns>Stack panel containing titled table</returns>
        private ExcelStackPanel CreateSemesterPanel(string title, DataTable table)
        {
            var termPanel = new ExcelStackPanel(Orientation.Vertical);
            termPanel.SpaceBetween = 0;
            var winterHeader = new ExcelText(title);
            var excelTable =  new ExcelTable(table);
            StyleTitleText(winterHeader);
            winterHeader.Size.Width = excelTable.Size.Width;
            termPanel.Items.Add(winterHeader);
            termPanel.Items.Add(new ExcelTable(table));
            return termPanel;
        }

        /// <summary>
        /// Styles the text for the report
        /// </summary>
        /// <param name="text">text instance for styling</param>
        private void StyleTitleText(ExcelText text)
        {
            text.Size.Height = 2;
            text.Styler.BackgroundColor = XlRgbColor.rgbDarkGray;
            text.Styler.FontColor = XlRgbColor.rgbWhiteSmoke;
            text.Styler.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            text.Styler.VerticalAlignment = XlVAlign.xlVAlignCenter;
            text.Styler.FontSize = 20;
        }
    }
}
