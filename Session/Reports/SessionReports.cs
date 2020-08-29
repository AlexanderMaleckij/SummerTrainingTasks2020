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
using System.Diagnostics;

namespace Session.Reports
{
    //sorts are available in Excel tables

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
                StyleTitleText(header);
                header.Size.Width = creditExam.Size.Width;
                var creditExamWithTitle = new ExcelStackPanel(Orientation.Vertical);
                creditExamWithTitle.Items.Add(header);
                creditExamWithTitle.Items.Add(creditExam);
                creditsExams.Items.Add(creditExamWithTitle);
            }

            var excelReport = new ExcelReport();
            excelReport.IsVisible = true;//DEBUG
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
            var groupSemesters = context.KnowledgeControls.ToList().Where(x => x.StudentGroupId == group.Id).Select(x => x.Semester).Distinct().ToList();
            List<string> expelledStudentsNames = new List<string>();
            foreach (int groupSemester in groupSemesters)
            {
                expelledStudentsNames.AddRange(new PivotDataTable(GetExamsTable(group, groupSemester)).ExpelledStudentsExams);
                expelledStudentsNames.AddRange(new PivotDataTable(GetCreditsTable(group, groupSemester)).ExpelledStudentsCredits);
            }
            expelledStudentsNames = expelledStudentsNames.Distinct().ToList();
            return context.Students.Where(x => expelledStudentsNames.Any(y => y == x.FullName)).ToList();
        }

        /// <summary>
        /// Prepare a table table of students and their marks by each passed subject
        /// </summary>
        /// <param name="group">group</param>
        /// <param name="semester">semester (1-winter 2-summer)</param>
        /// <returns>table with passing results</returns>
        private DataTable GetExamsTable(StudentGroup group, int semester)
        {
            var students = context.Students.
                Where(x => x.StudentGroupId == group.Id);
            var knowledgeControls = context.KnowledgeControls.
                Where(x => x.Semester == semester && x.StudentGroupId == group.Id);
            var examsRezults = Context.GetInstance().Exams.Where(x =>
                knowledgeControls.Any(t => t.Id == x.KnowledgeControlId) &&
                students.Any(z => z.Id == x.StudentId));
            var examsControls = knowledgeControls.GroupJoin(
                examsRezults,
                co => co.Id,
                ex => ex.KnowledgeControlId,
                (co, ex) => new
                {
                    co.SubjectName,
                    co.PassDate,
                    StudentsResults = examsRezults.Where(x => x.KnowledgeControlId == co.Id).ToList().
                        Select(exam => new {
                            students.Where(stud => stud.Id == exam.StudentId).First().FullName,
                            exam.Mark
                        })
                }).Where(x => x.StudentsResults.Count() != 0);
            #if DEBUG
            Debug.Print($"Exams of {group} in Semester:{semester}");
            foreach (var examControl in examsControls)
            {
                Debug.Print($"subject:{examControl.SubjectName}; pass date:{examControl.PassDate}");
                foreach (var passRez in examControl.StudentsResults)
                {
                    Debug.Print($"\tFullName:{passRez.FullName}; Mark:{passRez.Mark}");
                }
            }
            #endif
            var dtCreator = new PivotDataTable(
                "Student name", 
                students.Select(x => x.FullName).ToList(),
                examsControls.Select(x => x.SubjectName).ToList());
            examsControls.ToList().
                ForEach(examControl => examControl.StudentsResults.ToList().
                ForEach(studRez => dtCreator[studRez.FullName, examControl.SubjectName] = studRez.Mark.ToString()));
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
            var students = context.Students.
                Where(x => x.StudentGroupId == group.Id);
            var knowledgeControls = context.KnowledgeControls.
                Where(x => x.Semester == semester && x.StudentGroupId == group.Id);
            var creditsRezults = Context.GetInstance().Credits.Where(x =>
                knowledgeControls.Any(t => t.Id == x.KnowledgeControlId) &&
                students.Any(z => z.Id == x.StudentId));
            var creditsControls = knowledgeControls.GroupJoin(
                creditsRezults,
                co => co.Id,
                ex => ex.KnowledgeControlId,
                (co, ex) => new
                {
                    co.SubjectName,
                    co.PassDate,
                    StudentsResults = creditsRezults.Where(x => x.KnowledgeControlId == co.Id).ToList().
                        Select(exam => new {
                            students.Where(stud => stud.Id == exam.StudentId).First().FullName,
                            exam.IsPassed
                        })
                }).Where(x => x.StudentsResults.Count() != 0);

            var dtCreator = new PivotDataTable(
                "Student name",
                students.Select(x => x.FullName).ToList(),
                creditsControls.Select(x => x.SubjectName).ToList());
            creditsControls.ToList().
                ForEach(examControl => examControl.StudentsResults.ToList().
                ForEach(studRez => dtCreator[studRez.FullName, examControl.SubjectName] = studRez.IsPassed ? "Passed" : "Not passed"));
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
