using Readables.Common;
using Readables.Domain;
using Readables.UI;
using System.Collections.Generic;

namespace Readables.ViewModel.List
{
    public class ListViewModel: BaseViewModel
    {
        private readonly IReadableDataStore readableDataStore;

        public IEnumerable<Readable> Readables => this.readableDataStore.VisibleReadables;

        public ListViewModel()
        {
            this.readableDataStore = IOC.Resolve<IReadableDataStore>(); ;
        }
    }
}
