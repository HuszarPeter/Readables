using System;
using System.Linq;
using System.Collections.Generic;
using Readables.Domain;
using NLog;

namespace Readables.DataLayer
{
    public class ReadableRepository: IReadableRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        readonly IDataContext dataContext;

        public ReadableRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public IList<Readable> GetAllReadables()
        {
            logger.Debug($"Fetch all readables");
            return this.dataContext.Query<Readable>().ToList();
        }

        public void UpsertReadable(Readable readable)
        {
            logger.Info($"Upsert readable : {readable.Title}");
            this.dataContext.Upsert(readable);
        }
    }
}
