using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace Readables.ViewController
{
    public partial class ReadableView : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public ReadableView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        #endregion

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            WantsLayer = true;
            Layer.MasksToBounds = true;
        }
    }
}
