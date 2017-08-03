using System;
using System.Collections.Generic;

namespace Readables.DataLayer
{
    public interface IDataContext
    {
        void Insert<T>(T entity);
        void Upsert<T>(T entity);

        IEnumerable<Readables.Domain.Readable> Readables { get; }
    }
}
