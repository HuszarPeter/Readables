using Readables.UI.Model;

namespace Readables.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        private ViewMode contentMode;
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
    }
}
