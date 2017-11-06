using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Readables.DataLayer;
using Readables.Common;
using Readables.Domain;

namespace Readables.ViewControllers
{
    public partial class ReadableTableViewController : AppKit.NSViewController, INSTableViewDataSource, INSTableViewDelegate
    {
        private IReadableRepository readableRepository;
        private List<Readable> readables;

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
            var result = new NSImageView
            {
                ImageAlignment = NSImageAlignment.Left,
                ImageScaling = NSImageScale.None
            };
            var imageName = "format_comic";
            if (row % 3 == 0) {
                imageName = "format_epub";
            }
            else if (row % 3 == 1) 
            {
                imageName = "format_azw3";
            }
            result.Image = NSBundle.MainBundle.ImageForResource(imageName);
            return result;
        }
    }
}
