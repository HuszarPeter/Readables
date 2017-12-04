using System;
using System.Collections.Generic;
using System.Linq;
using Readables.Common;
using Readables.Common.Extensions;
using Readables.DataLayer;
using Readables.Domain;
using Readables.Import.AggregatedEvents;
using Readables.UI.AggregatedEvents;
using Readables.UI.Model;
using NLog;

namespace Readables.UI
{
    public class ReadableDataStore: IReadableDataStore, 
    IListenTo<FileImportedEvent>,
    IListenTo<PathImportedEvent>,
    IListenTo<FilterForStringRequest>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IEnumerable<Readable> visibleReadables;
        private readonly IEventAggregator eventAggregator;
        private readonly IReadableRepository readableRepository;
        private List<Readable> readables;

		private string FilterString;
        private UtilityItemLibrary LibraryFilter;
        private UtilityItemSubject SubjectFilter;

        public IEnumerable<Readable> VisibleReadables
        {
            get
            {
                this.FetchItems();
                return visibleReadables;
            }
        }

        public IEnumerable<UtilityItemSubject> Subjects
        {
            get
            {
                this.FetchItems();
                return readables
                    .SelectMany(r => r.Subjects)
                    .Where(subject => !String.IsNullOrEmpty(subject))
                    .GroupBy(subject => subject, StringComparer.InvariantCultureIgnoreCase)
                    .Select(grp => new UtilityItemSubject { Text = grp.Key, Count = grp.Count() })
                    .OrderBy(item => item.Text);
            }
        }

        public IEnumerable<UtilityItemLibrary> LibraryItems
        {
            get
            {
                this.FetchItems();
                return readables
                    .SelectMany(r => r.Files)
                    .GroupBy(file => file.Format, StringComparer.InvariantCultureIgnoreCase)
                    .Select(grp => new UtilityItemLibrary { Text = grp.Key, Count = grp.Count(), Filter = grp.Key });

            }
        }

        public ReadableDataStore(IEventAggregator eventAggregator, IReadableRepository readableRepository)
		{
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.readableRepository = readableRepository ?? throw new ArgumentNullException(nameof(readableRepository));

            this.eventAggregator.AddListener(this);
		}

        private void FetchItems()
        {
            if (this.readables != null)
            {
                return;
            }

            this.readables = this.readableRepository.GetAllReadables().ToList();
            this.MakeVisibleReadables();
        }

        private void MakeVisibleReadables()
        {
            this.visibleReadables = readables
                .OrderBy(r => r.Title);
            if (!string.IsNullOrEmpty(this.FilterString))
            {
                this.visibleReadables = this.visibleReadables
                    .Where(r => r.Title.Contains(this.FilterString, StringComparison.InvariantCultureIgnoreCase));
            }
            if (this.LibraryFilter != null && !string.IsNullOrEmpty(this.LibraryFilter.Filter))
            {
                this.visibleReadables = visibleReadables
                    .Where(r => r.Files.Any(file => file.Format == this.LibraryFilter.Filter));
            }
            if (this.SubjectFilter != null)
            {
                this.visibleReadables = this.visibleReadables
                    .Where(r => r.Subjects.Any(subj => subj == this.SubjectFilter.Text));
            }
            this.eventAggregator.SendMessage(new DataRepositoryChanged { Reason = DataRepositoryChangeReason.Filter });
        }

        public void HandleMessage(FileImportedEvent message)
        {
            this.readables = null;
            this.eventAggregator.SendMessage(new DataRepositoryChanged { Reason = DataRepositoryChangeReason.Import });
        }

        public void HandleMessage(PathImportedEvent message)
        {
            this.readables = null;
            this.eventAggregator.SendMessage(new DataRepositoryChanged { Reason = DataRepositoryChangeReason.Import });
        }

        public void SetLibraryItemFilter(UtilityItemLibrary item)
        {
            logger.Trace($"Set library item filter to: '{item.Filter}'");
            this.LibraryFilter = item;
            this.SubjectFilter = null;
            this.MakeVisibleReadables();
        }

        public void SetSubjectFilter(UtilityItemSubject subject)
        {
            logger.Trace($"Set subject filter to: '{subject.Text}'");
            this.LibraryFilter = null;
            this.SubjectFilter = subject;
            this.MakeVisibleReadables();
        }

        public void SetFilterString(string filter)
        {
            logger.Trace($"Set filter to: '{filter}'");
            this.FilterString = filter;
            this.MakeVisibleReadables();
        }

        public void HandleMessage(FilterForStringRequest message)
        {
            this.SetFilterString(message.FilterValue);
        }
    }
}
