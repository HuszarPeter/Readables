using System;
using System.Collections.Generic;
using System.Linq;
using Readables.Common;
using Readables.Data.Model;
using Readables.DataLayer;
using Readables.Domain;

namespace Readables.Data
{
    public interface IDataRepository
    {
        int NumberOfItemsInRepository();

        Readable ItemAtPosition(int index);

        void SetLibraryItemFilter(OutlineItemLibrary item);

        void SetSubjectFilter(OutlineItemSubject subject);
    }

    public class DataRepository: IDataRepository
    {
        readonly IEventAggregator eventAggregator;
        readonly IReadableRepository readableRepository;
        private List<Readable> readables;
        private IEnumerable<Readable> visibleReadables;

        public DataRepository(IEventAggregator eventAggregator, IReadableRepository readableRepository)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.readableRepository = readableRepository ?? throw new ArgumentNullException(nameof(readableRepository));
        }

        public int NumberOfItemsInRepository()
        {
            this.FetchItems();
            return this.visibleReadables.Count();
        }

        public Readable ItemAtPosition(int index)
        {
            this.FetchItems();
            var result = this.visibleReadables.ElementAt(index);
            return result;
        }

        public void SetLibraryItemFilter(OutlineItemLibrary item)
        {
            throw new NotImplementedException();
        }

        public void SetSubjectFilter(OutlineItemSubject subject)
        {
            throw new NotImplementedException();
        }

        private void FetchItems()
        {
            if (this.readables != null){
                return;
            }

            this.readables = this.readableRepository.GetAllReadables().ToList();
            this.visibleReadables = readables
                .OrderBy(r => r.Title);
        }
    }
}
