using System;
using System.Collections.Generic;

namespace Readables.DataLayer
{
    public interface IDataContext
    {
        void Insert<T>(T entity);
        void Upsert<T>(T entity);


        IEnumerable<T> Query<T>(string collectionName = null);

    }
}
