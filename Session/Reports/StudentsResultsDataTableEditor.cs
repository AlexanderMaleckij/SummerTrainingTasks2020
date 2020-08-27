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
                Select(stidentMarks => stidentMarks.ItemArray.Skip(2).Select(x => int.Parse((string)x)).Sum()).Sum();
                int amountOfStudents = DataTable.Rows.Count;
                return sumOfMarks / amountOfStudents;
            }
        }
        public double MinMark
        {
            get
            {
                return DataTable.Rows.OfType<DataRow>().ToList().
                Select(stidentMarks => stidentMarks.ItemArray.Skip(2).Select(x => int.Parse((string)x)).Min()).Min();
            }
        }
        public double MaxMark
        {
            get
            {
                return DataTable.Rows.OfType<DataRow>().ToList().
               Select(stidentMarks => stidentMarks.ItemArray.Skip(2).Select(x => int.Parse((string)x)).Max()).Max();
            }
        }
        public List<string> ExpelledStudents
        {
            get
            {
                List<string> expelledStudents = new List<string>();
                foreach(DataRow row in DataTable.Rows)
                {
                    if(row.ItemArray.Skip(2).Select(x => int.Parse((string)x)).Any(x => x < 4))
                    {
                        expelledStudents.Add((string)row.ItemArray[1]);
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
            DataTable.Columns.Add("№");
            DataTable.Columns.Add(rowsName);
            DataTable.Columns["№"].AutoIncrement = true;
            colNames.ForEach(colName => DataTable.Columns.Add(colName));
            rowNames.Sort();
            rowNames.ForEach(x => DataTable.Rows.Add(x));
        }

        public PivotDataTable(DataTable dataTable)
        {
            DataTable = dataTable;
        }
    }
}
