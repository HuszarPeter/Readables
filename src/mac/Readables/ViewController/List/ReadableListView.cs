using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace Readables.ViewController.List
{
    public partial class ReadableListView : AppKit.NSView
    {
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
        }

        #endregion
    }
}
