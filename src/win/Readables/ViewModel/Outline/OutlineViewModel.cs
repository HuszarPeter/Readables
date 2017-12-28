using AutoMapper;
using Readables.Common;
using Readables.UI;
using Readables.UI.AggregatedEvents;
using Readables.UI.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Readables.ViewModel.Outline
{
    public class OutlineViewModel : BaseViewModel, IListenTo<DataRepositoryChanged>
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
        private readonly IEventAggregator eventAggregator;

        public OutlineItemBase SelectedOutlineItem
        {
            get
            {
                return selectedOutlineItem;
            }
            set
            {
                OnPropertyChanging(nameof(SelectedOutlineItem));
                selectedOutlineItem = value;
                if (selectedOutlineItem is OutlineLibraryItem itm)
                {
                    this.readableDataStore.SetLibraryItemFilter(Mapper.Map<UtilityItemLibrary>(itm));
                }
                else if (selectedOutlineItem is OutlineSubject subj)
                {
                    this.readableDataStore.SetSubjectFilter(Mapper.Map<UtilityItemSubject>(subj));
                }
                OnPropertyChanged(nameof(SelectedOutlineItem));
            }
        }

        public OutlineViewModel()
        {
            this.readableDataStore = IOC.Resolve<IReadableDataStore>();
            this.eventAggregator = IOC.Resolve<IEventAggregator>();
            this.eventAggregator.AddListener(this);

            this.CreateData();
        }

        private void CreateData()
        {
            this.selectedOutlineItem = null;

            var realLibraryItems = this.readableDataStore.LibraryItems.Select(i => Mapper.Map<OutlineLibraryItem>(i));
            var libraryItems = new[] {
                new OutlineLibraryItem
                {
                    Text = "All items",
                    Count = realLibraryItems.Sum(i => i.Count),
                    Filter = string.Empty
                }
            }
            .Union(realLibraryItems)
            .ToArray();

            this.Outline = new[]
            {
                new OutlineGroup
                {
                    Text = "Library",
                    IsExpanded = true,
                    Items = libraryItems
                },
                new OutlineGroup
                {
                    Text = "Subjects",
                    IsExpanded = true,
                    Items = this.readableDataStore.Subjects.Select(s => Mapper.Map<OutlineSubject>(s)).ToArray()
                }
            };
        }

        public void HandleMessage(DataRepositoryChanged message)
        {
            if (message.Reason == DataRepositoryChangeReason.Filter)
            {
                return;
            }

            this.CreateData();
        }
    }
}
