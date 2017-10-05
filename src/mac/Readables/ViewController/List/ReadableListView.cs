using System;
using Foundation;
using AppKit;
using Readables.Import;
using Readables.Common;
using Readables.Utils;

namespace Readables.ViewController.List
{
    public partial class ReadableListView : AppKit.NSView
    {
        private IImportService importService;
        private PathDropHandler pathDropHelper;


        #region Constructors

        // Called when created from unmanaged code
        public ReadableListView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableListView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
            this.importService = IOC.Container.Resolve<IImportService>();
        }

        public override void ViewDidMoveToSuperview()
        {
            base.ViewDidMoveToSuperview();
			this.pathDropHelper = new PathDropHandler(this, this.importService);
        }

        #endregion

        public override NSDragOperation DraggingEntered(NSDraggingInfo sender)
        {
            return this.pathDropHelper.GetAvailableDraggingOperation(sender);
        }

        public override bool PerformDragOperation(NSDraggingInfo sender)
        {
            return this.pathDropHelper.PerformDrop(sender);
        }
    }
}
