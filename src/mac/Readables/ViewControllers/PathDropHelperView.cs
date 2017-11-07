using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Readables.Import;
using Readables.Utils;
using Readables.Common;

namespace Readables.ViewControllers
{
    public partial class PathDropHelperView : AppKit.NSView
    {
        private IImportService importService;
        private PathDropHandler pathDropHelper;

        #region Constructors

        // Called when created from unmanaged code
        public PathDropHelperView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public PathDropHelperView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
            this.importService = IOC.Container.Resolve<IImportService>();
        }

        #endregion

        public override void ViewDidMoveToSuperview()
        {
            base.ViewDidMoveToSuperview();
            this.pathDropHelper = new PathDropHandler(this, this.importService);
        }

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
