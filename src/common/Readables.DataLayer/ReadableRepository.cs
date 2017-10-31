using System;
using System.Linq;
using System.Collections.Generic;
using Readables.Domain;

namespace Readables.DataLayer
{
    public class ReadableRepository: IReadableRepository
    {
        readonly IDataContext dataContext;

        public ReadableRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public IList<Readable> GetAllReadables()
        {
            return this.dataContext.Query<Readable>().ToList();
        }

        public void UpsertReadable(Readable readable)
        {
            this.dataContext.Upsert(readable);
        }
    }
}
