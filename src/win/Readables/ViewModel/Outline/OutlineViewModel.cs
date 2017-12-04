using Readables.Utils;
using System.Collections.Generic;
using System.Windows.Input;
using System;

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


        private OutlineItemBase selectedOutlineItem;
        public OutlineItemBase SelectedOutlineItem
        {
            get
            {
                return selectedOutlineItem;
            }
            set
            {
                Console.WriteLine($"Prev: {selectedOutlineItem}, new: {value}");
                OnPropertyChanging(nameof(SelectedOutlineItem));
                selectedOutlineItem = value;
                OnPropertyChanged(nameof(SelectedOutlineItem));
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
                            Text = "Books",
                            Count = 1
                        }
                    }
                },
                new OutlineGroup
                {
                    Text = "Subjects",
                    IsExpanded = true,
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

    public class OutlineLibraryItem: OutlineItemBase
    {
    }

    public class OutlineSubject: OutlineItemBase
    {
    }
}
