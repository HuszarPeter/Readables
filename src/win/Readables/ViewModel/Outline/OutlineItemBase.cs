namespace Readables.ViewModel.Outline
{
    public abstract class OutlineItemBase: BaseViewModel
    {
        private string text;
        private int count;

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                OnPropertyChanging(nameof(Text));
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                OnPropertyChanging(nameof(Count));
                count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
    }
}
