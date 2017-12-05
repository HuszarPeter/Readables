using Readables.Common;
using Readables.UI;
using Readables.UI.AggregatedEvents;
using Readables.UI.Model;

namespace Readables.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        private readonly IEventAggregator eventAgregator;

        private ViewMode contentMode;

        private string searchText;

        public ViewMode ContentMode
        {
            get
            {
                return contentMode;
            }
            set
            {
                OnPropertyChanging(nameof(ContentMode));
                contentMode = value;
                OnPropertyChanged(nameof(ContentMode));
            }
        }

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                OnPropertyChanging(nameof(SearchText));
                searchText = value;
                this.eventAgregator.SendMessage(new FilterForStringRequest { FilterValue = value });
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public MainViewModel()
        {
            this.eventAgregator = IOC.Resolve<IEventAggregator>();
        }
    }
}
