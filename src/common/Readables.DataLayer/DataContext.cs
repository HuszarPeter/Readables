using System;
using System.Collections.Generic;
using System.IO;

namespace Readables.DataLayer
{
    public class DataContext : LiteDB.LiteRepository, IDataContext
    {
        static string dbPath = "readables.db";

        public DataContext() : base(dbPath)
        {
        }

        public IEnumerable<Readables.Domain.Readable> Readables 
        { 
            get
            {
                return this.Query<Readables.Domain.Readable>()
                           .ToEnumerable();
            }
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
