using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Session
{
    class DataTableEditor
    {
        private DataTable dataTable;
        private readonly string rowsName;

        public DataTable DataTable
        {
            get => dataTable;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException($"{nameof(DataTable)} can't be null");
                }

                dataTable = value;
            }
        }
        public double AverageValue
        {
            get
            {
                int sumOfValues = DataTable.Rows.OfType<DataRow>().ToList().
                Select(row => row.ItemArray.Skip(1).Select(col => int.Parse((string)col)).Sum()).Sum();
                int amountOfRecords = DataTable.Rows.Count;
                return sumOfValues / amountOfRecords;
            }
        }
        public double MinValue
        {
            get
            {
                return DataTable.Rows.OfType<DataRow>().ToList().
                Select(row => row.ItemArray.Skip(1).Select(col => int.Parse((string)col)).Min()).Min();
            }
        }
        public double MaxValue
        {
            get
            {
                return DataTable.Rows.OfType<DataRow>().ToList().
               Select(row => row.ItemArray.Skip(1).Select(col => int.Parse((string)col)).Max()).Max();
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
            foreach (DataRow row in DataTable.Rows)
            {
                if ((string)row[rowsName] == rowName)
                {
                    return row;
                }
            }

            return null;
        }

        public DataTableEditor(string rowsName, List<string> rowNames, List<string> colNames)
        {
            DataTable = new DataTable();
            this.rowsName = rowsName;
            DataTable.Columns.Add(rowsName);
            colNames.ForEach(colName => DataTable.Columns.Add(colName));
            rowNames.ForEach(x => DataTable.Rows.Add(x));
            rowNames.Sort();
        }

        public DataTableEditor(DataTable dataTable)
        {
            DataTable = dataTable;
        }
    }
}
