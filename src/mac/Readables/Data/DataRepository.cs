using System;
using System.Collections.Generic;
using System.Linq;
using Readables.AggregatedEvents;
using Readables.Common;
using Readables.Data.Model;
using Readables.DataLayer;
using Readables.Domain;
using Readables.Import.AggregatedEvents;

namespace Readables.Data
{
    public interface IDataRepository
    {
        IEnumerable<Readable> VisibleReadables { get; }

        void SetLibraryItemFilter(OutlineItemLibrary item);

        void SetSubjectFilter(OutlineItemSubject subject);
    }

    public class DataRepository: IDataRepository,
    IListenTo<FileImportedEvent>,
    IListenTo<PathImportedEvent>
    {
        readonly IEventAggregator eventAggregator;
        readonly IReadableRepository readableRepository;
        private List<Readable> readables;

        private IEnumerable<Readable> visibleReadables;
        public IEnumerable<Readable> VisibleReadables
        {
            get
            {
                this.FetchItems();
                return visibleReadables;    
            }
        }

        public DataRepository(IEventAggregator eventAggregator, IReadableRepository readableRepository)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.readableRepository = readableRepository ?? throw new ArgumentNullException(nameof(readableRepository));

            this.eventAggregator.AddListener(this);
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

        #region Aggregated event handling
        public void HandleMessage(FileImportedEvent message)
        {
            this.readables = null;
            this.visibleReadables = null;
            this.eventAggregator.SendMessage(new DataRepositoryChanged());
        }

        public void HandleMessage(PathImportedEvent message)
        {
            this.readables = null;
            this.visibleReadables = null;
            this.eventAggregator.SendMessage(new DataRepositoryChanged());
        }
        #endregion
    }
}
