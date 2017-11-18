using System;
using System.Linq;
using AppKit;
using Foundation;
using Readables.Common;
using Readables.Data.Model;
using Readables.Data;

namespace Readables.ViewControllers.Outline
{
    [Register(nameof(ReadableOutlineDataSource))]
    public class ReadableOutlineDataSource : NSObject, INSOutlineViewDataSource
    {
        readonly OutlineGroup LibraryGroup;
        readonly OutlineGroup SubjectsGroup;
        readonly OutlineGroup[] groups;

        readonly IDataRepository dataRepository;

        public ReadableOutlineDataSource() : this(IOC.Resolve<IDataRepository>())
        {
        }

        public ReadableOutlineDataSource(IDataRepository dataRepository)
        {
            this.LibraryGroup = new OutlineGroup { Text = "Library" };
            this.SubjectsGroup = new OutlineGroup { Text = "Subjects" };

            this.groups = new[] { LibraryGroup, SubjectsGroup };
            this.dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));

            LoadData();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

        public void ReloadData() 
        {
            this.LoadData();
        }

        private void LoadData()
        {

            var allLibraryItem = new[] { new OutlineItemLibrary { Text = "All readables", Count = this.dataRepository.LibraryItems.Sum(i => i.Count), Filter = string.Empty } };

            this.LibraryGroup.Items = allLibraryItem.Union(
                this.dataRepository.LibraryItems)
                .ToArray();

            this.SubjectsGroup.Items = this.dataRepository
                .Subjects
                .ToArray();

        }

        [Export("outlineView:isGroupItem:")]
        public bool IsGroupItem(NSOutlineView outlineView, NSObject item)
        {
            return item is OutlineGroup;
        }

        [Export("outlineView:numberOfChildrenOfItem:")]
        public nint GetChildrenCount(NSOutlineView outlineView, NSObject item)
        {
            if (item is OutlineGroup)
            {
                return ((OutlineGroup)item).Items.Length;
            }

            return this.groups.Length;
        }

        [Export("outlineView:isItemExpandable:")]
        public bool ItemExpandable(NSOutlineView outlineView, NSObject item)
        {
            return item is OutlineGroup;
        }

        [Export("outlineView:child:ofItem:")]
        public NSObject GetChild(NSOutlineView outlineView, nint childIndex, NSObject item)
        {
            if (item is OutlineGroup outline)
            {
                return outline.Items[childIndex];
            }

            return this.groups[childIndex];
        }

        [Export("outlineView:objectValueForTableColumn:byItem:")]
        public NSObject GetObjectValue(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            return item;
        }
    }
}
