﻿using System;
using System.Collections.Generic;
using System.Linq;
using Readables.AggregatedEvents;
using Readables.Common;
using Readables.Data.Model;
using Readables.DataLayer;
using Readables.Domain;
using Readables.Import.AggregatedEvents;

namespace Readables.Data
{
    public class DataRepository: IDataRepository,
    IListenTo<FileImportedEvent>,
    IListenTo<PathImportedEvent>
    {
        readonly IEventAggregator eventAggregator;
        readonly IReadableRepository readableRepository;
        private List<Readable> readables;

        private IEnumerable<Readable> visibleReadables;
        public IEnumerable<Readable> VisibleReadables
        {
            get
            {
                this.FetchItems();
                return visibleReadables;    
            }
        }

        public IEnumerable<OutlineItemSubject> Subjects
        {
            get
            {
                this.FetchItems();
                return readables
                    .SelectMany(r => r.Subjects)
                    .Where(subject => !String.IsNullOrEmpty(subject))
                    .GroupBy(subject => subject, StringComparer.InvariantCultureIgnoreCase)
                    .Select(grp => new OutlineItemSubject { Text = grp.Key, Count = grp.Count() })
                    .OrderBy(item => item.Text);
            }
        }

        public IEnumerable<OutlineItemLibrary> LibraryItems 
        {
            get
            {
                this.FetchItems();
                return readables
                    .SelectMany(r => r.Files)
                    .GroupBy(file => file.Format, StringComparer.InvariantCultureIgnoreCase)
                    .Select(grp => new OutlineItemLibrary { Text = grp.Key, Count = grp.Count(), Filter = grp.Key });
            }
        }


        public DataRepository(IEventAggregator eventAggregator, IReadableRepository readableRepository)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.readableRepository = readableRepository ?? throw new ArgumentNullException(nameof(readableRepository));

            this.eventAggregator.AddListener(this);
        }

        public void SetLibraryItemFilter(OutlineItemLibrary item)
        {
            this.MakeVisibleReadables();
            if (!string.IsNullOrEmpty(item.Filter))
            {
                this.visibleReadables = visibleReadables
                    .Where(r => r.Files.Any(file => file.Format == item.Text));
            }
            this.eventAggregator.SendMessage(new DataRepositoryChanged { Reason = DataRepositoryChangeReason.Filter });
        }

        public void SetSubjectFilter(OutlineItemSubject subject)
        {
            this.MakeVisibleReadables();
            this.visibleReadables = this.visibleReadables
                .Where(r => r.Subjects.Any(subj => subj == subject.Text));
            this.eventAggregator.SendMessage(new DataRepositoryChanged { Reason = DataRepositoryChangeReason.Filter });
        }

        private void FetchItems()
        {
            if (this.readables != null){
                return;
            }

            this.readables = this.readableRepository.GetAllReadables().ToList();
            this.MakeVisibleReadables();
        }

        private void MakeVisibleReadables()
        {
            this.visibleReadables = readables
                .OrderBy(r => r.Title);
        }

        #region Aggregated event handling
        public void HandleMessage(FileImportedEvent message)
        {
            this.readables = null;
            this.visibleReadables = null;
            this.eventAggregator.SendMessage(new DataRepositoryChanged { Reason = DataRepositoryChangeReason.Import});
        }

        public void HandleMessage(PathImportedEvent message)
        {
            this.readables = null;
            this.visibleReadables = null;
            this.eventAggregator.SendMessage(new DataRepositoryChanged { Reason = DataRepositoryChangeReason.Import });
        }
        #endregion
    }
}
