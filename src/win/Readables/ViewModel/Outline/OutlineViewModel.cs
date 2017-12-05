using AutoMapper;
using Readables.Common;
using Readables.UI;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private readonly IReadableDataStore readableDataStore;

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
            this.readableDataStore = IOC.Resolve<IReadableDataStore>();

            outline = new[]
            {
                new OutlineGroup
                {
                    Text = "Library",
                    IsExpanded = true,
                    Items = this.readableDataStore.LibraryItems.Select(i => Mapper.Map<OutlineLibraryItem>(i)).ToArray()
                },
                new OutlineGroup
                {
                    Text = "Subjects",
                    IsExpanded = true,
                    Items = this.readableDataStore.Subjects.Select(s => Mapper.Map<OutlineSubject>(s)).ToArray()
                }
            };
        }
    }
}
