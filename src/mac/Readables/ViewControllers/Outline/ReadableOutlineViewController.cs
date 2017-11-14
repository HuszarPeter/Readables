using System;
using AppKit;
using Foundation;
using Readables.AggregatedEvents;
using Readables.Common;
using Readables.Import.AggregatedEvents;
using Readables.ViewControllers.Outline;
using Readables.ViewControllers.Outline.Model;

namespace Readables.ViewControllers
{
    public partial class ReadableOutlineViewController : NSViewController, INSOutlineViewDelegate,
    IListenTo<FileImportedEvent>, 
    IListenTo<PathImportedEvent>
    {
        private IEventAggregator eventAggregator;

        // Called when created from unmanaged code
        public ReadableOutlineViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableOutlineViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
            this.eventAggregator = IOC.Resolve<IEventAggregator>();
            this.eventAggregator.AddListener(this);
        }

        [Export("outlineView:isGroupItem:")]
        public bool IsGroupItem(NSOutlineView outlineView, NSObject item)
        {
            return item is Group;
        }

        [Export("outlineView:viewForTableColumn:item:")]
        public NSView GetView(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            if (item is Group outline)
            {
                var view = outlineView.MakeView("HeaderCell", this) as NSTableCellView;
                view.TextField.StringValue = outline.Text;
                view.TextField.ToolTip = view.TextField.StringValue;
                return view;
            }
            if (item is LibraryItem f) 
            {
                var view = outlineView.MakeView("DataCellWithBadge", this) as ReadableOutlineDataCellView;
                view.TextField.StringValue = $"{f.Text}";
                view.TextField.ToolTip = view.TextField.StringValue;
                view.BadgeText.StringValue = $"{f.Count}";
                return view;
            }
            if (item is Subject it)
            {
                var view = outlineView.MakeView("DataCellWithBadge", this) as ReadableOutlineDataCellView;
                view.TextField.StringValue = $"{it.Text}";
                view.TextField.ToolTip = view.TextField.StringValue;
                view.BadgeText.StringValue = $"{it.Count}";
                return view;
            }
            return null;
        }

        [Export("outlineViewSelectionDidChange:")]
        public void SelectionDidChange(NSNotification notification)
        {
            var index = this.outlineView.SelectedRows;
            var itemAtRow = this.outlineView.ItemAtRow((nint)index.FirstIndex);
            if (itemAtRow is LibraryItem item) 
            {
                this.eventAggregator.SendMessage(new FilterForLibraryItemRequest{ Format = item.Text });
            }
            if (itemAtRow is Subject subject) 
            {
                this.eventAggregator.SendMessage(new FilterForSubjectRequest { Subject = subject.Text });
            }

        }

        public void HandleMessage(PathImportedEvent message)
        {
            ((ReadableOutlineDataSource)this.outlineView.DataSource).ReloadData();
            this.outlineView.ReloadData();
        }

        public void HandleMessage(FileImportedEvent message)
        {
            ((ReadableOutlineDataSource)this.outlineView.DataSource).ReloadData();
            this.outlineView.ReloadData();
        }
    }
}
