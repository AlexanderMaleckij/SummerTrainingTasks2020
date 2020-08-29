using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataTable = System.Data.DataTable;

namespace Session.Reports
{
    class PivotDataTable
    {
        private DataTable dataTable;
        private readonly string rowsName;
        public DataTable DataTable 
        {
            get => dataTable;
            private set
            {
                if(value == null)
                {
                    throw new ArgumentException($"{nameof(DataTable)} can't be null");
                }

                dataTable = value;
            }
        }

        public double AverageMark
        {
            get
            {
                int sumOfMarks = DataTable.Rows.OfType<DataRow>().ToList().
                Select(stidentMarks => stidentMarks.ItemArray.Skip(1).Select(x => int.Parse((string)x)).Sum()).Sum();
                int amountOfStudents = DataTable.Rows.Count;
                return sumOfMarks / amountOfStudents;
            }
        }
        public double MinMark
        {
            get
            {
                return DataTable.Rows.OfType<DataRow>().ToList().
                Select(stidentMarks => stidentMarks.ItemArray.Skip(1).Select(x => int.Parse((string)x)).Min()).Min();
            }
        }
        public double MaxMark
        {
            get
            {
                return DataTable.Rows.OfType<DataRow>().ToList().
               Select(stidentMarks => stidentMarks.ItemArray.Skip(1).Select(x => int.Parse((string)x)).Max()).Max();
            }
        }
        public List<string> ExpelledStudentsExams
        {
            get
            {
                List<string> expelledStudents = new List<string>();
                foreach(DataRow row in DataTable.Rows)
                {
                    if(row.ItemArray.Skip(1).Select(x => int.Parse((string)x)).Any(x => x < 4))
                    {
                        expelledStudents.Add((string)row.ItemArray[0]);
                    }
                }

                return expelledStudents;
            }
        }

        public List<string> ExpelledStudentsCredits
        {
            get
            {
                List<string> expelledStudents = new List<string>();
                foreach (DataRow row in DataTable.Rows)
                {
                    if (row.ItemArray.Skip(1).Any(x => (string)x == "Not passed"))
                    {
                        expelledStudents.Add((string)row.ItemArray[0]);
                    }
                }

                return expelledStudents;
            }
        }

        public string this[string rowName, string colName]
        {
            get
            {
                return (string)FindRow(rowName)[colName];
            }
            set
            {
                FindRow(rowName)[colName] = value;
            }
        }

        private DataRow FindRow(string rowName)
        {
            foreach(DataRow row in DataTable.Rows)
            {
                if((string)row[rowsName] == rowName)
                {
                    return row;
                }
            }

            return null;
        }

        public PivotDataTable(string rowsName, List<string> rowNames, List<string> colNames)
        {
            DataTable = new DataTable();
            this.rowsName = rowsName;
            DataTable.Columns.Add(rowsName);
            colNames.ForEach(colName => DataTable.Columns.Add(colName));
            rowNames.ForEach(x => DataTable.Rows.Add(x));
            rowNames.Sort();
        }

        public PivotDataTable(DataTable dataTable)
        {
            DataTable = dataTable;
        }
    }
}
