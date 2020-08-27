using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ORM
{
    class OneToOneMapper<T> : IMapper<T> where T : class, new()
    {
        private PropertyInfo[] Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
        private readonly DataSet dataSet;

        public OneToOneMapper(DataSet dataSet)
        {
            this.dataSet = dataSet;
        }

        public T Map(DataRow dataRow)
        {
            if (!dataSet.Tables.Contains($"{typeof(T).Name}"))
            {
                throw new Exception($"Can't map {dataRow.GetType()} to {typeof(T)}. \n" +
                                    $"DataSet not contains table called '{typeof(T).Name}'");
            }

            if (IsCompatibleClassAndTable(dataRow.Table.TableName))
            {
                T result = new T();
                foreach (PropertyInfo property in Properties)
                {
                    property.SetValue(result, dataRow[property.Name]);
                }

                return result;
            }
            else
            {
                throw new Exception($"Can't map {dataRow.GetType()} to {typeof(T)}");
            }
        }

        public IEnumerable<T> Map(DataTable table)
        {
            List<T> entities = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                if(row.RowState != DataRowState.Deleted)
                {
                    entities.Add(Map(row));
                }
            }

            return entities;
        }

        public DataRow Map(T instance)
        {
            if (!dataSet.Tables.Contains($"{typeof(T).Name}"))
            {
                throw new Exception($"Can't map {typeof(T).Name} to DataRow. \n" +
                                    $"DataSet not contains table called '{typeof(T).Name}'");
            }

            if (IsCompatibleClassAndTable(typeof(T).Name))
            {
                DataRow newRow = dataSet.Tables[$"{typeof(T).Name}"].NewRow();
                foreach (PropertyInfo property in Properties)
                {
                    if (property.Name != "Id")
                    {
                        newRow[$"{property.Name}"] = property.GetValue(instance);
                    }
                }

                return newRow;
            }
            else
            {
                throw new Exception($"Can't map {typeof(T).Name} to DataRow.");
            }
        }

        public List<DataRow> Map(IEnumerable<T> collection)
        {
            List<DataRow> dataRows = new List<DataRow>();

            foreach (T instance in collection)
            {
                dataRows.Add(Map(instance));
            }

            return dataRows;
        }

        private List<string> GetNamesOfTableColumns(string tableName)
        {
            return dataSet.
                Tables[$"{typeof(T).Name}"].Columns.
                Cast<DataColumn>().
                Select(x => x.ColumnName).ToList();
        }

        private bool IsCompatibleClassAndTable(string tableName)
        {
            return Properties.Select(x => x.Name).Except(GetNamesOfTableColumns($"{typeof(T).Name}")).Count() == 0;
        }
    }
}
