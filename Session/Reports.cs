using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Excel;
using Excel.Containers;
using Excel.Items;
using Excel.Properties;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace Session
{
    public class Reports
    {
        private readonly SessionDataContext context;

        public Reports(SessionDataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Prepare ExcelReport of session results 
        /// for each group in the form of a table
        /// </summary>
        /// <param name="course">sort option by group course</param>
        /// <param name="studentsNamesInTable">sort option by students names in tables rows (top down)</param>
        /// <param name="examsNamesInTable">sort option by exams names in tables columns (left right)</param>
        /// <returns>report instance</returns>
        public ExcelReport ReportLastSessionAllGroups(Sort course = Sort.AscendingOrder, 
                                                      Sort studentsNamesInTable = Sort.AscendingOrder, 
                                                      Sort examsNamesInTable =    Sort.AscendingOrder)
        {
            var creditsExams = new ExcelStackPanel(Orientation.Vertical);
            var groups = StudentsGroupsSort(context.StudentGroups.ToList(), course);

            foreach (var group in groups)
            {
                var controls = context.KnowledgeControls.Where(x => x.StudentGroups == group).ToList();
                if(controls.Count != 0)
                {
                    int latestSemester = (int)controls.Select(x => x.Semester).Max();
                    var creditExam = new ExcelStackPanel(Orientation.Horizintal);
                    creditExam.Items.Add(new ExcelTable(GetExamsTable(group, latestSemester, studentsNamesInTable, examsNamesInTable)));
                    creditExam.Items.Add(new ExcelTable(GetCreditsTable(group, latestSemester, studentsNamesInTable, examsNamesInTable)));
                    var header = new ExcelText($"{group.GroupName} group (Semester №{latestSemester})");
                    StyleTitleText(header);
                    header.Size.Width = creditExam.Size.Width;
                    var creditExamWithTitle = new ExcelStackPanel(Orientation.Vertical);
                    creditExamWithTitle.Items.Add(header);
                    creditExamWithTitle.Items.Add(creditExam);
                    creditsExams.Items.Add(creditExamWithTitle);
                }
            }
            var excelReport = new ExcelReport();
            excelReport.AddReportItem(creditsExams);
            return excelReport;
        }

        /// <summary>
        /// Prepare a table table of students and their marks by each passed subject
        /// </summary>
        /// <param name="group">group</param>
        /// <param name="semester">semester (1-winter 2-summer)</param>
        /// <returns>table with passing results</returns>
        private DataTable GetExamsTable(StudentGroups group, int semester, Sort studentsNamesInTable, Sort examsNamesInTable)
        {
            var exams = context.Exams.ToList().Where(x => group.KnowledgeControls.ToList().Any(z => x.KnowledgeControlId == z.Id && z.Semester == semester)).ToList();
            var dtEditor = new DataTableEditor(
                "Student name",
                TableNamesSort(group.Students.ToList().Select(x => x.FullName).ToList(), studentsNamesInTable),
                TableNamesSort(exams.Select(x => x.KnowledgeControls).Select(x => x.Subjects.SubjectName).Distinct().ToList(), examsNamesInTable));
            exams.ToList().ForEach(studRez => dtEditor[studRez.Students.FullName, studRez.KnowledgeControls.Subjects.SubjectName] = studRez.Mark.ToString());
            return dtEditor.DataTable;
        }

        /// <summary>
        /// Prepare a table with the results of passing credits
        /// </summary>
        /// <param name="group">group</param>
        /// <param name="semester">semester</param>
        /// <returns>table with passing results</returns>
        private DataTable GetCreditsTable(StudentGroups group, int semester, Sort studentsNamesInTable, Sort creditsNamesInTable)
        {
            var credits = context.Credits.ToList().Where(x => group.KnowledgeControls.ToList().Any(z => x.KnowledgeControlId == z.Id && z.Semester == semester)).ToList();
            var dtEditor = new DataTableEditor(
                "Student name",
                TableNamesSort(group.Students.Select(x => x.FullName).ToList(), studentsNamesInTable),
                TableNamesSort(credits.Select(x => x.KnowledgeControls).Select(x => x.Subjects.SubjectName).Distinct().ToList(), creditsNamesInTable));
            credits.ToList().ForEach(studRez => dtEditor[studRez.Students.FullName, studRez.KnowledgeControls.Subjects.SubjectName] = (bool)studRez.IsPassed ? "Passed" : "Not passed");
            return dtEditor.DataTable;
        }

        /// <summary>
        /// Form excel report for each session, display: 
        /// - a pivot table  with the average / minimum / maximum mark for each group
        /// - a table with an average mark for each specialty
        /// - a table with the average mark of each teacher
        /// </summary>
        /// <param name="year">>sort option by academic year</param>
        /// <param name="studentsNamesInTable">sort option by students names in tables rows (top down)</param>
        /// <param name="examsNamesInTable">sort option by exams names in tables columns (left right)</param>
        /// <param name="specialitiesNamesInTable">sort option by specialties names in tables columns (left right)</param>
        /// <param name="teachersNamesInTable">sort option by teachers names in tables rows (top down)</param>
        /// <returns>report instance</returns>
        public ExcelReport ReportPivotTablesEachSessionEachGroup(Sort year = Sort.DescendingOrder,
                                                      Sort studentsNamesInTable = Sort.AscendingOrder,
                                                      Sort examsNamesInTable = Sort.AscendingOrder,
                                                      Sort specialitiesNamesInTable = Sort.AscendingOrder,
                                                      Sort teachersNamesInTable = Sort.AscendingOrder)
        {
            var stackPanel = new ExcelStackPanel(Orientation.Vertical){ SpaceBetween = 2 }; //main container
            var groups = StudentsGroupsSort(context.StudentGroups.ToList(), year);
            foreach (var academicYearGroups in groups.GroupBy(x => x.SeptemberYear))
            {
                stackPanel.Items.Add(
                    CreateSessionPanel(
                        academicYearGroups.Key,
                        1,
                        GetGroupsMarksTable(academicYearGroups.ToList(), 1, studentsNamesInTable, examsNamesInTable),
                        GetAverageMarksBySpecialties(academicYearGroups.ToList(), 1, specialitiesNamesInTable),
                        GetAverageMarkOfTeachers(academicYearGroups.ToList(), 1, teachersNamesInTable)));
                stackPanel.Items.Add(
                    CreateSessionPanel(
                        academicYearGroups.Key,
                        2,
                        GetGroupsMarksTable(academicYearGroups.ToList(), 2, studentsNamesInTable, examsNamesInTable),
                        GetAverageMarksBySpecialties(academicYearGroups.ToList(), 2, specialitiesNamesInTable),
                        GetAverageMarkOfTeachers(academicYearGroups.ToList(), 2, teachersNamesInTable)));
            }

            var report = new ExcelReport();
            report.AddReportItem(stackPanel);
            return report;
        }

        /// <summary>
        /// Creates a stack panel with header and given tables
        /// </summary>
        /// <param name="year">year of transition to the new course</param>
        /// <param name="semester">semester</param>
        /// <param name="groupsMarks">table with marks of groups</param>
        /// <param name="averageMarksBySpecialties">table with average marks by specialties</param>
        /// <param name="averageMarkOfTeachers">table with average marks by teachers</param>
        /// <returns></returns>
        private ExcelStackPanel CreateSessionPanel(int year, int semester, DataTable groupsMarks, DataTable averageMarksBySpecialties, DataTable averageMarkOfTeachers)
        {
            var mainPanel = new ExcelStackPanel(Orientation.Vertical);
            var header = new ExcelText($"Session {year} - {year + 1} Semester {semester}");
            StyleTitleText(header);
            var groupsMarksExcelTable = new ExcelTable(groupsMarks);
            header.Size.Width = groupsMarksExcelTable.Size.Width;
            mainPanel.Items.Add(header);
            mainPanel.Items.Add(groupsMarksExcelTable);
            mainPanel.Items.Add(new ExcelTable(averageMarksBySpecialties));
            mainPanel.Items.Add(new ExcelTable(averageMarkOfTeachers));
            return mainPanel;
        }

        /// <summary>
        /// Build a table with the average / minimum /
        /// maximum mark for given groups in specified semester
        /// </summary>
        /// <param name="studentGroups">students groups</param>
        /// <param name="semester">semester number</param>
        /// <param name="examsNamesInTable">sort option by exams names in tables columns (left right)</param>
        /// <param name="studentsNamesInTable">sort option by students names in tables rows (top down)</param>
        /// <returns>pivot table</returns>
        private DataTable GetGroupsMarksTable(List<StudentGroups> studentGroups, int semester, Sort studentsNamesInTable, Sort examsNamesInTable)
        {
            DataTableEditor dataTable = new DataTableEditor(
                "Mark",
                new List<string>() { "Average", "Minimal", "Maximal" },
                studentGroups.Select(x => x.GroupName).ToList());

            foreach (var group in studentGroups)
            {
                var studentResults = new DataTableEditor(GetExamsTable(group, semester, studentsNamesInTable, examsNamesInTable));
                dataTable["Average", group.GroupName] = studentResults.AverageValue.ToString();
                dataTable["Minimal", group.GroupName] = studentResults.MinValue.ToString();
                dataTable["Maximal", group.GroupName] = studentResults.MaxValue.ToString();
            }

            return dataTable.DataTable;
        }

        /// <summary>
        /// creates a table with an average mark for each specialty to which the given groups belong
        /// </summary>
        /// <param name="studentGroups">groups</param>
        /// <param name="semester">semester (1 or 2)</param>
        /// <param name="specialitiesNamesInTable">sort option by specialties names in tables columns (left right)</param>
        /// <returns>table</returns>
        private DataTable GetAverageMarksBySpecialties(List<StudentGroups> studentGroups, int semester, Sort specialitiesNamesInTable)
        {
            var specialtiesGroups = studentGroups.GroupBy(x => x.Specialties);
            var dtEditor = new DataTableEditor(
               " ",
               new List<string>() { "Average mark"},
               TableNamesSort(specialtiesGroups.Select(x => x.Key.SpecialtyName).ToList(), specialitiesNamesInTable));
            foreach(var specialtyGroups in specialtiesGroups)
            {
                List<KnowledgeControls> specialtyGroupsControls = new List<KnowledgeControls>();
                specialtyGroups.ToList().ForEach(x => specialtyGroupsControls.AddRange(x.KnowledgeControls));
                specialtyGroupsControls = specialtyGroupsControls.Where(x => x.Semester == semester).ToList();
                var exams = context.Exams.ToList().Where(exam => specialtyGroupsControls.Any(control => control.Id == exam.KnowledgeControlId)).ToList();
                dtEditor["Average mark", specialtyGroups.Key.SpecialtyName] = (exams.Select(x => (int)x.Mark).Sum() / exams.Count).ToString();
            }
            return dtEditor.DataTable;
        }

        /// <summary>
        /// creates a table with the average mark of each teacher who taught at least one of the specified groups
        /// </summary>
        /// <param name="studentGroups">groups</param>
        /// <param name="semester">semester (1 or 2)</param>
        /// <param name="teachersNamesInTable">sort option by teachers names in tables rows (top down)</param>
        /// <returns>table</returns>
        private DataTable GetAverageMarkOfTeachers(List<StudentGroups> studentGroups, int semester, Sort teachersNamesInTable)
        {
            var controls = studentGroups.Select(x => x.KnowledgeControls).ToList();
            List<Teachers> teachers = new List<Teachers>();
            controls.ForEach(x => x.ToList().ForEach(y => teachers.Add(y.Teachers)));
            var dtEditor = new DataTableEditor(
                "Teacher name",
                TableNamesSort(teachers.Select(x => x.FullName).Distinct().ToList(), teachersNamesInTable),
                new List<string>() { "Average mark"});
            foreach(var teacher in teachers)
            {
                var teacherControls = context.KnowledgeControls.ToList().Where(x => x.TeacherId == teacher.Id && x.Semester == semester).ToList();
                var teacherExams = context.Exams.ToList().Where(exam => teacherControls.Any(control => exam.KnowledgeControlId == control.Id));
                if(teacherExams.Count() != 0)
                {
                    dtEditor[teacher.FullName, "Average mark"] = (teacherExams.Select(x => (int)x.Mark).Sum() / teacherExams.Count()).ToString();
                }
                else
                {
                    dtEditor[teacher.FullName, "Average mark"] = "-";
                }
            }
            return dtEditor.DataTable;
        }

        /// <summary>
        /// Create report with dynamics of change in the average mark for each subject by years
        /// </summary>
        /// <param name="subjectsNamesRows">sort option by subjects names in tables columns (top down)</param>
        /// <param name="yearsCols">sort option by academic years in tables columns (left right)</param>
        /// <returns>report</returns>
        public ExcelReport DynamicSubjectsMarksChangeByYears(Sort subjectsNamesRows = Sort.AscendingOrder, 
                                                             Sort yearsCols = Sort.AscendingOrder)
        {
            var years = context.StudentGroups.ToList().Select(x => x.SeptemberYear).Distinct().ToList();
            var sessionsColNames = new List<string>();
            years.ForEach(year => sessionsColNames.AddRange(new List<string>() { $"{year} Winter", $"{year} Summer" }));
            sessionsColNames = TableNamesSort(sessionsColNames, yearsCols);
            var dtEditor = new DataTableEditor(
               "Subject",
               TableNamesSort(context.Exams.ToList().
               Select(x => x.KnowledgeControls).
               Select(x => x.Subjects).
               Select(x => x.SubjectName).
               Distinct().ToList(), subjectsNamesRows),
               sessionsColNames);
            
            foreach(var subject in context.Subjects.ToList())
            {
                foreach(var year in years)
                {
                    foreach(var semester in Enumerable.Range(1, 2))
                    {
                        int sumOfMarks = 0, amountOfMarks = 0;
                        foreach (var exam in context.Exams.ToList())
                        {
                            if (exam.KnowledgeControls.StudentGroups.SeptemberYear == year && 
                                exam.KnowledgeControls.SubjectId == subject.Id && 
                                exam.KnowledgeControls.Semester == semester)
                            {   //if found an exam in the right subject in the right year in the right semester
                                sumOfMarks += exam.Mark;
                                amountOfMarks++;
                            }
                        }
                        if(amountOfMarks != 0)
                        {
                            dtEditor[subject.SubjectName, $"{year} {(semester == 1 ? "Winter" : "Summer")}"] = (sumOfMarks / amountOfMarks).ToString();
                        }
                    }
                }
            }
            var table = new ExcelTable(dtEditor.DataTable);
            var header = new ExcelText("Dynamics");
            StyleTitleText(header);
            header.Size.Width = table.Size.Width;
            var stackPanel = new ExcelStackPanel(Orientation.Vertical);
            stackPanel.Items.Add(header);
            stackPanel.Items.Add(table);
            var report = new ExcelReport();
            report.AddReportItem(stackPanel);
            return report;
        }

        /// <summary>
        /// Prepare expelled students list of a given group
        /// </summary>
        /// <param name="group">students group</param>
        /// <returns>list of expelled students</returns>
        public List<Students> GetExpelledStudents(StudentGroups group)
        {
            var groupSemesters = context.KnowledgeControls.ToList().Where(x => x.StudentGroupId == group.Id).Select(x => x.Semester).Distinct().ToList();
            List<string> expelledStudentsNames = new List<string>();
            foreach (int groupSemester in groupSemesters)
            {
                expelledStudentsNames.AddRange(GetExpelledStudentsExams(GetExamsTable(group, groupSemester, Sort.AscendingOrder, Sort.AscendingOrder)));
                expelledStudentsNames.AddRange(GetExpelledStudentsCredits(GetCreditsTable(group, groupSemester, Sort.AscendingOrder, Sort.AscendingOrder)));
            }
            expelledStudentsNames = expelledStudentsNames.Distinct().ToList();
            return context.Students.ToList().Where(x => expelledStudentsNames.Any(y => y == x.FullName)).ToList();
        }

        private List<string> GetExpelledStudentsCredits(DataTable table)
        {
            List<string> expelledStudents = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                if (row.ItemArray.Skip(1).Any(x => (string)x == "Not passed"))
                {
                    expelledStudents.Add((string)row.ItemArray[0]);
                }
            }
            return expelledStudents;
        }
        
        private List<string> GetExpelledStudentsExams(DataTable table)
        {
            List<string> expelledStudents = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                if (row.ItemArray.Skip(1).Select(x => int.Parse((string)x)).Any(x => x < 4))
                {
                    expelledStudents.Add((string)row.ItemArray[0]);
                }
            }
            return expelledStudents;
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

        private List<StudentGroups> StudentsGroupsSort(List<StudentGroups> groups, Sort courseOrder)
        {
            switch (courseOrder)
            {
                case Sort.AscendingOrder:
                    return groups.OrderBy(x => x.SeptemberYear).ToList();
                case Sort.DescendingOrder:
                    return groups.OrderByDescending(x => x.SeptemberYear).ToList();
                default:
                    throw new ArgumentException("sorting courses in the given order is not supported");
            }
        }
        private List<string> TableNamesSort(List<string> names, Sort namesOrder)
        {
            switch(namesOrder)
            {
                case Sort.AscendingOrder:
                    return names.OrderBy(x => x).ToList();
                case Sort.DescendingOrder:
                    return names.OrderByDescending(x => x).ToList();
                default:
                    throw new ArgumentException("sorting names in the given order is not supported");
            }
        }
    }
}
