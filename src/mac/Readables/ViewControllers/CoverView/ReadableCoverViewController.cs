using System;
using AppKit;
using Foundation;

namespace Readables.ViewControllers.Collection
{
    [Register(nameof(ReadableCoverViewController))]
    public partial class ReadableCoverViewController : NSViewController
    {
        public ReadableCoverViewController()
        {
        }
        // Called when created from unmanaged code
        public ReadableCoverViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableCoverViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }
    }
}
