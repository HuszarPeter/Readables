using System.Collections.Generic;

namespace Readables.ViewModel.Outline
{
    public class OutlineGroup: BaseViewModel
    {
        private string text;
        private IEnumerable<OutlineItemBase> items;
        private bool isExpanded;

        public string Text {
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

        public IEnumerable<OutlineItemBase> Items
        {
            get
            {
                return items;
            }
            set
            {
                OnPropertyChanging(nameof(Items));
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                OnPropertyChanging(nameof(IsExpanded));
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }
    }
}
