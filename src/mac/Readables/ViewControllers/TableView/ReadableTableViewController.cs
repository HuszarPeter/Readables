using System;
using System.Linq;
using AppKit;
using Foundation;
using Readables.AggregatedEvents;
using Readables.Common;
using Readables.UI.AggregatedEvents;

namespace Readables.ViewControllers.TableView
{
    public partial class ReadableTableViewController : NSViewController,
    INSTableViewDataSource,
    INSTableViewDelegate,
    IEventAggregatorSubscriber,
    IListenTo<FilterForLibraryItemRequest>,
    IListenTo<FilterForSubjectRequest>,
    IListenTo<DataRepositoryChanged>
    {
        private ReadableTableViewPresenter presenter;
        private ReadableListInteractor interactor;
        private IEventAggregator eventAggregator;

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
            this.interactor = new ReadableListInteractor();

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
            return this.interactor.Readables.Count();
        }

        [Export("tableView:viewForTableColumn:row:")]
        public NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var readable = this.interactor.Readables.ElementAt((int)row);

            return this.presenter.CellViewForColumn(tableView, tableColumn.Identifier, (int)row, readable);
        }
        #endregion

        #region Message handling
        public void HandleMessage(DataRepositoryChanged message)
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
