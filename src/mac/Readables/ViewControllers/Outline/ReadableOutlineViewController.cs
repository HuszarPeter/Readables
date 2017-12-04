using System;
using AppKit;
using Foundation;
using Readables.Common;
using Readables.ViewControllers.Outline;
using Readables.ViewControllers.Outline.Cells;
using Readables.UI.AggregatedEvents;
using Readables.UI;
using AutoMapper;
using Readables.ViewControllers.Outline.Model;

namespace Readables.ViewControllers
{
    public partial class ReadableOutlineViewController : NSViewController, INSOutlineViewDelegate, IListenTo<DataRepositoryChanged>
    {
        private IEventAggregator eventAggregator;
        private IReadableDataStore dataRepository;

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
            this.dataRepository = IOC.Resolve<IReadableDataStore>();
            this.eventAggregator.AddListener(this);
        }

        [Export("outlineView:isGroupItem:")]
        public bool IsGroupItem(NSOutlineView outlineView, NSObject item)
        {
            return item is OutlineGroup;
        }

        [Export("outlineView:viewForTableColumn:item:")]
        public NSView GetView(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            if (item is OutlineGroup outline)
            {
                var view = outlineView.MakeView("HeaderCell", this) as NSTableCellView;
                view.TextField.StringValue = outline.Text;
                view.TextField.ToolTip = view.TextField.StringValue;
                return view;
            }
            if (item is OutlineItemLibrary f) 
            {
                var view = outlineView.MakeView("DataCellWithBadge", this) as ReadableOutlineDataCellView;
                view.TextField.StringValue = $"{f.Text}";
                view.TextField.ToolTip = view.TextField.StringValue;
                view.BadgeText.StringValue = $"{f.Count}";
                return view;
            }
            if (item is OutlineItemSubject it)
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
            if (itemAtRow is OutlineItemLibrary item) 
            {
                this.dataRepository.SetLibraryItemFilter(Mapper.Map<UI.Model.UtilityItemLibrary>(item));
            }
            if (itemAtRow is OutlineItemSubject subject) 
            {
                this.dataRepository.SetSubjectFilter(Mapper.Map<UI.Model.UtilityItemSubject>(subject));
            }

        }

        public void HandleMessage(DataRepositoryChanged message)
        {
            if (message.Reason == DataRepositoryChangeReason.Filter)
            {
                return;
            }
            
            ((ReadableOutlineDataSource)this.outlineView.DataSource).ReloadData();
            this.outlineView.ReloadData();
        }
    }
}
