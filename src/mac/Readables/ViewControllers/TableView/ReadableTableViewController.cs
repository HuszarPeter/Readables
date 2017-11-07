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

namespace Readables.ViewControllers.TableView
{
    public partial class ReadableTableViewController : AppKit.NSViewController, INSTableViewDataSource, INSTableViewDelegate, IListenTo<FileImportedEvent>, IListenTo<PathImportedEvent>
    {
        private IReadableRepository readableRepository;
        private List<Readable> readables;
		private IEventAggregator eventAggregator;

        private const string TextCellIdentifier = "textColumnCell";
        private const string ImageCellIdentifier = "imageColumnCell";

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
            this.readables = this.readableRepository.GetAllReadables().ToList();
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
                                Identifier = ImageCellIdentifier,
                                Readable = readable,
                            };
                        }
                        return result;
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

        [Export("tableView:sortDescriptorsDidChange:")]
        public void SortDescriptorsChanged(NSTableView tableView, NSSortDescriptor[] oldDescriptors)
        {
            Console.WriteLine("sort");
            foreach (var sortDescriptor in tableView.SortDescriptors)
            {
                Console.WriteLine($"Key: {sortDescriptor.Key} - Acending:{sortDescriptor.Ascending}");
            }
            tableView.ReloadData();
        }

        private ReadableTextTableCellView MakeTextCellView(Readable readable, ReadableFieldView field) {
            var result = this.tableView.MakeView(TextCellIdentifier, this) as ReadableTextTableCellView;
            if (result == null) {
                result = new ReadableTextTableCellView
                {
                    Identifier = TextCellIdentifier,
                    Readable = readable,
                    Field = field
                };
            }
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
    }
}
