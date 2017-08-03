using System;

using Foundation;
using AppKit;
using Readables.ViewController;

namespace Readables
{
    public partial class MainWindowController : NSWindowController
    {
        public MainWindowController(IntPtr handle) : base(handle)
        {
        }

        [Export("initWithCoder:")]
        public MainWindowController(NSCoder coder) : base(coder)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

        public new MainWindow Window
        {
            get { return (MainWindow)base.Window; }
        }

        partial void mainViewSelectorChanged(NSSegmentedControl sender)
        {
            var viewController = this.Window.ContentViewController as Readables.BooksViewController;
            if (viewController != null)
            {
                viewController.ViewMode = (BooksViewMode)((int)sender.SelectedSegment);
            }
        }
    }
}
