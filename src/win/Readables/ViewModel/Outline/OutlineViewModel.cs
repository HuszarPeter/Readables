using System.Collections.Generic;

namespace Readables.ViewModel.Outline
{
    public class OutlineViewModel : BaseViewModel
    {
        private IEnumerable<OutlineGroup> outline;

        public IEnumerable<OutlineGroup> Outline
        {
            get
            {
                return outline;
            }
            set
            {
                OnPropertyChanging(nameof(Outline));
                outline = value;
                OnPropertyChanged(nameof(Outline));
            }
        }

        public OutlineViewModel()
        {
            outline = new[]
            {
                new OutlineGroup
                {
                    Text = "Library",
                    IsExpanded = true,
                    Items = new[] {
                        new OutlineLibraryItem
                        {
                            Text = "Comics"
                        },
                        new OutlineLibraryItem
                        {
                            Text = "Books"
                        }
                    }
                },
                new OutlineGroup
                {
                    Text = "Subjects",
                    Items = new[] {
                        new OutlineSubject
                        {
                            Text = "subj 1",
                            Count = 10000
                        },
                        new OutlineSubject
                        {
                            Text = "subj2",
                            Count = 3
                        }
                    }
                }
            };
        }
    }

    public abstract class OutlineItemBase
    {
        public string Text { get; set; }

        public int Count { get; set; }
    }

    public class OutlineLibraryItem: OutlineItemBase
    {
    }

    public class OutlineSubject: OutlineItemBase
    {
    }
}
