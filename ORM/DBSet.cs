using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace ORM
{
    //Represents Crearor class (in factory method pattern diagram)
    //https://refactoring.guru/images/patterns/diagrams/factory-method/structure.png


    public abstract class DBSet<T> : IUnitOfWork, IRepository<T>, IEnumerable<T> where T : ModelBase
    {
        SqlConnection SqlConnection { get; set; }
        SqlDataAdapter Adapter { get; set; } 
        DataSet DataSet { get; set; }

        public DBSet(DataSet dataSet, SqlConnection connection) 
        {
            if(connection == null)
            {
                throw new ArgumentException($"{nameof(connection)} can't be null");
            }

            if(dataSet == null)
            {
                throw new ArgumentException($"{nameof(dataSet)} can't be null");
            }

            SqlConnection = connection;
            DataSet = dataSet;
            Adapter = new SqlDataAdapter($"SELECT * FROM {typeof(T).Name}", SqlConnection);
        }

        public abstract IRepository<T> FactoryMethod();

        public void Add(T item) => FactoryMethod().Add(item);

        public void Clear() => FactoryMethod().Clear();

        public void Remove(T item) => FactoryMethod().Remove(item);

        public IEnumerator GetEnumerator() => FactoryMethod().GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>)GetEnumerator();

        void IUnitOfWork.Commit()
        {
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(Adapter);
            Adapter.Update(DataSet, typeof(T).Name);
        }

        protected void LoadDbSet()
        {
            LoadSchema();
            LoadData();
            LoadRelations();
        }

        private void LoadSchema()
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            var dataTable = new DataTable(typeof(T).Name);

            foreach (PropertyDescriptor p in props)
            {
                var column = dataTable.Columns.Add(p.Name, p.PropertyType);

                if (p.Name == "Id")
                {
                    column.AutoIncrement = true;
                    dataTable.PrimaryKey = new DataColumn[] { column };
                }
            }
            DataSet.Tables.Add(dataTable);
        }

        private void LoadData()
        {
            Adapter.Fill(DataSet, typeof(T).Name);
            Adapter.FillSchema(DataSet, SchemaType.Source);
        }

        private void LoadRelations()
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            var relations = new List<DataRelation>();

            foreach (PropertyDescriptor p in props)
            {
                if (p.Name != "Id" && p.Name.EndsWith("Id")) //all id fields except PK
                {
                    string tableForBinding = p.Name.Substring(0, p.Name.Length - 2);
                    string relationName = $"{typeof(T).Name}{tableForBinding}";
                    DataColumn firstRelationColumn = DataSet.Tables[$"{tableForBinding}"].Columns["Id"];
                    DataColumn secondRelationColumn = DataSet.Tables[typeof(T).Name].Columns[$"{tableForBinding}Id"];
                    DataRelation relation = new DataRelation(relationName, firstRelationColumn, secondRelationColumn);
                    relations.Add(relation);
                }
            }

            DataSet.Relations.AddRange(relations.ToArray());
        }
    }
}
