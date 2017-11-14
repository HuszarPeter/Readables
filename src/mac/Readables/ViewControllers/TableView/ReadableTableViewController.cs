using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Readables.DataLayer;
using Readables.Common;
using Readables.Domain;
using Readables.Extensions;
using Readables.Import.AggregatedEvents;
using Readables.Import;
using Readables.Utils;
using Readables.AggregatedEvents;

namespace Readables.ViewControllers.TableView
{
    public partial class ReadableTableViewController : AppKit.NSViewController, INSTableViewDataSource, INSTableViewDelegate, 
    IListenTo<FileImportedEvent>, IListenTo<PathImportedEvent>, IListenTo<FilterForLibraryItemRequest>, IListenTo<FilterForSubjectRequest>
    {
        private IReadableRepository readableRepository;
        private List<Readable> readables;
		private IEventAggregator eventAggregator;

        private const string TextCellIdentifier = "textColumnCell";
        private const string ImageCellIdentifier = "imageColumnCell";

        private string formatFilter;
        private string subjectFilter;

        #region Constructors

        // Called when created from unmanaged code
        public ReadableTableViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableTableViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
            this.readableRepository = IOC.Container.Resolve<IReadableRepository>();
            this.eventAggregator = IOC.Container.Resolve<IEventAggregator>();
            this.eventAggregator.AddListener(this);
            this.ReadReadables();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

        private void ReadReadables() {
            var readableList = this.readableRepository
                                   .GetAllReadables()
                                   .AsEnumerable();
            
            if (!string.IsNullOrEmpty(this.formatFilter)) 
            {
                readableList = readableList.Where(r => r.Files.Any(f => f.Format == this.formatFilter));
            }

            if (!string.IsNullOrEmpty(this.subjectFilter)) 
            {
                readableList = readableList.Where(r => r.Subjects.Any(s => s == this.subjectFilter));
            }

            this.readables = readableList
                .OrderBy(r => r.Title)
                .ToList();
        }

        #endregion

        [Export("numberOfRowsInTableView:")]
        public nint GetRowCount(NSTableView tableView)
        {
            return this.readables.Count();
        }  

        [Export("tableView:viewForTableColumn:row:")]
        public NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var readable = this.readables[(int)row];

            switch(tableColumn.Identifier) 
            {
                case "titleColumn": 
                    {
                        return MakeTextCellView(readable, ReadableFieldView.Title);
                    }
                case "formatsColumn":
                    {
                        var result = this.tableView.MakeView(ImageCellIdentifier, this) as ReadableTagsTableCellView;
                        if (result == null)
                        {
                            result = new ReadableTagsTableCellView()
                            {
                                Identifier = ImageCellIdentifier
                            };
                        }
                        result.Readable = readable;
                        return result;
                    }
                case "seriesColumn":
                    {
                        return MakeTextCellView(readable, ReadableFieldView.Series);
                    }
                case "dateAddedColumn":
                    {
                        return MakeTextCellView(readable, ReadableFieldView.DateAdded);
                    }
                case "authorColumn":
                    {
                        return MakeTextCellView(readable, ReadableFieldView.Author);
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        private ReadableTextTableCellView MakeTextCellView(Readable readable, ReadableFieldView field) {
            var result = this.tableView.MakeView(TextCellIdentifier, this) as ReadableTextTableCellView;
            if (result == null) {
                result = new ReadableTextTableCellView
                {
                    Identifier = TextCellIdentifier
                };
            }
            result.Readable = readable;
            result.Field = field;
            return result;
        }

        public void HandleMessage(PathImportedEvent message)
        {
            this.ReadReadables();
            this.tableView.ReloadData();
        }

        public void HandleMessage(FileImportedEvent message)
        {
            this.ReadReadables();
            this.tableView.ReloadData();
        }

        public void HandleMessage(FilterForLibraryItemRequest message)
        {
            this.formatFilter = message.Format == "All readables" ? string.Empty : message.Format;
            this.subjectFilter = string.Empty;
            this.ReadReadables();
            this.tableView.ReloadData();
        }

        public void HandleMessage(FilterForSubjectRequest message)
        {
            this.formatFilter = string.Empty;
            this.subjectFilter = message.Subject;
            this.ReadReadables();
            this.tableView.ReloadData();
        }
    }
}
