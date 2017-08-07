using System;
using System.Collections.Generic;
using System.IO;

namespace Readables.DataLayer
{
    public class DataContext : IDataContext
    {
        static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "readables.db");

        public IEnumerable<Readables.Domain.Readable> Readables 
        { 
            get
            {
                using(var repo = new LiteDB.LiteRepository(dbPath))
                {
                    return repo.Query<Readables.Domain.Readable>().ToArray();
                }
            }
        }

        public void Insert<T>(T entity)
        {
            using(var repo = new LiteDB.LiteRepository(dbPath))
            {
                repo.Insert(entity);
            }
        }

        public void Upsert<T>(T entity)
        {
            using(var repo = new LiteDB.LiteRepository(dbPath))
            {
                repo.Upsert(entity);
            }
        }
    }
}
