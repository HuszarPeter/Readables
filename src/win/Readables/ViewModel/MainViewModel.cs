namespace Readables.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        private ContentMode contentMode;
        public ContentMode ContentMode
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
