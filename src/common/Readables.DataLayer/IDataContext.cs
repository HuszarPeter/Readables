using System;
namespace Readables.DataLayer
{
    public interface IDataContext
    {
        void Insert<T>(T entity);
        void Upsert<T>(T entity);
    }
}
