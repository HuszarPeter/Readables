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
using Readables.ViewControllers.TableView.Cells;

namespace Readables.ViewControllers.TableView
{
    public partial class ReadableTableViewController : NSViewController,
    INSTableViewDataSource,
    INSTableViewDelegate,
    IEventAggregatorSubscriber,
    IListenTo<FileImportedEvent>,
    IListenTo<PathImportedEvent>,
    IListenTo<FilterForLibraryItemRequest>,
    IListenTo<FilterForSubjectRequest>
    {
        private ReadableTableViewPresenter presenter;
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
            this.presenter = new ReadableTableViewPresenter();
            this.eventAggregator = IOC.Container.Resolve<IEventAggregator>();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

        #endregion

        #region DataSource
        [Export("numberOfRowsInTableView:")]
        public nint GetRowCount(NSTableView tableView)
        {
            return this.presenter.ItemsCount();
        }

        [Export("tableView:viewForTableColumn:row:")]
        public NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            return this.presenter.ItemAt((int)row, tableColumn.Identifier, tableView);
        }
        #endregion

        #region Message handling
        public void HandleMessage(PathImportedEvent message)
        {
            this.tableView.ReloadData();
        }

        public void HandleMessage(FileImportedEvent message)
        {
            this.tableView.ReloadData();
        }

        public void HandleMessage(FilterForLibraryItemRequest message)
        {
            this.formatFilter = message.Format == "All readables" ? string.Empty : message.Format;
            this.subjectFilter = string.Empty;
            this.tableView.ReloadData();
        }

        public void HandleMessage(FilterForSubjectRequest message)
        {
            this.formatFilter = string.Empty;
            this.subjectFilter = message.Subject;
            this.tableView.ReloadData();
        }
        #endregion

        public void SubscribeToAggregatedEvents()
        {
            this.eventAggregator.AddListener(this);
        }

        public void UnSubscribeFromAggregatedEvents()
        {
            this.eventAggregator.RemoveListener(this);
        }
    }
}
