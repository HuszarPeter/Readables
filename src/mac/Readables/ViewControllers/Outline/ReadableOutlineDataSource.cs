using System;
using System.Linq;
using AppKit;
using Foundation;
using Readables.Common;
using Readables.DataLayer;
using Readables.ViewControllers.Outline.Model;

namespace Readables.ViewControllers.Outline
{
    [Register(nameof(ReadableOutlineDataSource))]
    public class ReadableOutlineDataSource : NSObject, INSOutlineViewDataSource
    {
        readonly Group LibraryGroup;
        readonly Group SubjectsGroup;
        readonly Group[] groups;

        readonly IReadableRepository readableRepository;

        public ReadableOutlineDataSource() : this(IOC.Resolve<IReadableRepository>())
        {
        }

        public ReadableOutlineDataSource(IReadableRepository readableRepository)
        {
            Console.WriteLine("ctor");
            this.LibraryGroup = new Group { Text = "Library" };
            this.SubjectsGroup = new Group { Text = "Subjects" };

            this.groups = new[] { LibraryGroup, SubjectsGroup };
            this.readableRepository = readableRepository ?? throw new ArgumentNullException(nameof(readableRepository));

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
            var readables = this.readableRepository.GetAllReadables();

            var allLibraryItem = new[] { new LibraryItem { Text = "All readables", Count = readables.Count } };

            this.LibraryGroup.Items = allLibraryItem.Union(
                readables
                .SelectMany(r => r.Files)
                .Where(file => !String.IsNullOrEmpty(file.Format))
                .GroupBy(file => file.Format, StringComparer.InvariantCultureIgnoreCase)
                .Select(x => new LibraryItem { Text = x.Key, Count = x.Count() })
                .OrderBy(format => format.Text))
                .ToArray();

            this.SubjectsGroup.Items = readables
                .SelectMany(r => r.Subjects)
                .Where(subj => !string.IsNullOrEmpty(subj))
                .GroupBy(subj => subj, StringComparer.InvariantCultureIgnoreCase)
                .Select(grp => new Subject { Text = grp.Key, Count = grp.Count() })
                .OrderBy(s => s.Text)
                .ToArray();

        }

        [Export("outlineView:isGroupItem:")]
        public bool IsGroupItem(NSOutlineView outlineView, NSObject item)
        {
            return item is Group;
        }

        [Export("outlineView:numberOfChildrenOfItem:")]
        public nint GetChildrenCount(NSOutlineView outlineView, NSObject item)
        {
            if (item is Group)
            {
                return ((Group)item).Items.Length;
            }

            return this.groups.Length;
        }

        [Export("outlineView:isItemExpandable:")]
        public bool ItemExpandable(NSOutlineView outlineView, NSObject item)
        {
            return item is Group;
        }

        [Export("outlineView:child:ofItem:")]
        public NSObject GetChild(NSOutlineView outlineView, nint childIndex, NSObject item)
        {
            if (item is Group outline)
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
