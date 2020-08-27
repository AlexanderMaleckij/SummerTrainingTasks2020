using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace ORM
{
    //Represents all concrete products (in factory method pattern diagram) 
    //https://refactoring.guru/images/patterns/diagrams/factory-method/structure.png

    /// <summary>
    /// Сontains the basic logic for working with the DataTable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : ModelBase, new()
    {
        private DataTable Table { get; set; }
        private IMapper<T> Mapper { get; set; }
        public IEnumerable<T> ItemsCopy { get => Mapper.Map(Table); }

        public Repository(DataSet dataSet)
        {
            Mapper = new OneToOneMapper<T>(dataSet);
            Table = dataSet.Tables[typeof(T).Name];
        }

        /// <summary>
        /// Add new dataTable row
        /// </summary>
        /// <param name="item">item to add</param>
        public void Add(T item)
        {
            if (Mapper.Map(Table).Any(x => x.Equals(item)))
            {
                throw new Exception($"Table {typeof(T).Name} already contains this record");
            }
            else
            {
                DataRow newRow = Mapper.Map(item);
                Table.Rows.Add(newRow);
            }
        }

        /// <summary>
        /// Clear all dataTable items
        /// </summary>
        public void Clear() => Table.Clear();

        /// <summary>
        /// Remove one item from dataTable
        /// </summary>
        /// <param name="item">item to remove</param>
        public void Remove(T item)
        {
            T oldItem = ItemsCopy.Where(x => x.Equals(item)).FirstOrDefault();

            if (oldItem != null)
            {
                DataRow oldRow = Table.Select($"Id = {oldItem.Id}").First();
                oldRow.Delete();
            }
        }

        public IEnumerator GetEnumerator()
        {
            IEnumerable<T> items = ItemsCopy;

            foreach (T item in items)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }

            return items.GetEnumerator();
        }

        /// <summary>
        /// model class property change handler
        /// </summary>
        /// <param name="sender">model class instance</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            T item = sender as T;
            DataRow row = Table.Select($"Id = {item.Id}").FirstOrDefault();
            if (row != null)
            {
                row[e.PropertyName] = typeof(T).GetProperty(e.PropertyName).GetValue(item);
            }
        }
    }
}
