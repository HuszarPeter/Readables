using System;
using System.IO;

namespace Readables.DataLayer
{
    public class DataContext : LiteDB.LiteRepository, IDataContext
    {
        static string dbPath = "readables.db";

        public DataContext() : base(dbPath)
        {
        }

        public void Insert<T>(T entity)
        {
            base.Insert(entity);
        }

        public void Upsert<T>(T entity)
        {
            base.Upsert(entity);
        }
    }
}
