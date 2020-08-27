using System.Collections.Generic;
using System.Data;

namespace ORM
{
    interface IMapper<T> where T : class, new()
    {
        T Map(DataRow dataRow);
        IEnumerable<T> Map(DataTable table);
        DataRow Map(T instance);
        List<DataRow> Map(IEnumerable<T> collection);
    }
}
