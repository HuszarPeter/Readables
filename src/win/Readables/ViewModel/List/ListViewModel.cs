using Readables.Common;
using Readables.Domain;
using Readables.UI;
using Readables.UI.AggregatedEvents;
using System.Collections.Generic;

namespace Readables.ViewModel.List
{
    public class ListViewModel: BaseViewModel, IEventAggregatorSubscriber, IListenTo<DataRepositoryChanged>
    {
        private readonly IReadableDataStore readableDataStore;
        private readonly IEventAggregator eventAggregator;

        public IEnumerable<Readable> Readables => this.readableDataStore.VisibleReadables;

        public ListViewModel()
        {
            this.readableDataStore = IOC.Resolve<IReadableDataStore>();
            this.eventAggregator = IOC.Resolve<IEventAggregator>();
        }

        public void HandleMessage(DataRepositoryChanged message)
        {
            OnPropertyChanged(nameof(Readables));
        }

        public void SubscribeToAggregatedEvents()
        {
            this.eventAggregator.AddListener(this);
        }

        public void UnSubscribeFromAggregatedEvents()
        {
            this.eventAggregator.RemoveListener(this);
        }
    }
}
