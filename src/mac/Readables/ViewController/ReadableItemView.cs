using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Readables.Domain;

namespace Readables.ViewController
{
    public partial class ReadableItemView : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public ReadableItemView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableItemView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

        #endregion

        private Readable readable;
        public Readable Readable 
        {
            get
            {
                return readable;
            }
            set
            {
                WillChangeValue(nameof(Readable));
                readable = value;
                this.titleLabel.StringValue = readable.Title;
                this.authorLabel.StringValue = readable.Author ?? "";
                DidChangeValue(nameof(Readable));
            }
        }
    }
}
