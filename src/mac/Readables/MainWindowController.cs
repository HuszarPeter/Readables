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

            this.changeView.SelectedSegment = 0;
            this.UpdateViewController();
        }

        public new MainWindow Window
        {
            get { return (MainWindow)base.Window; }
        }

        partial void onViewChanged(NSSegmentedControl sender)
        {
            this.UpdateViewController();
        }

        private void UpdateViewController()
        {
            switch (this.changeView.SelectedSegment)
            {
                case 0:
                    this.Window.ContentViewController = new ViewController.List.ReadableListViewController();
                    break;
                case 1:
                    this.Window.ContentViewController = new ReadableViewController();
                    break;
                default:
                    break;
            }
        }
    }
}
