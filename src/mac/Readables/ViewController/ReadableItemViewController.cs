using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Readables.Domain;

namespace Readables.ViewController
{
    public partial class ReadableItemViewController : NSCollectionViewItem
    {
        #region Constructors

        // Called when created from unmanaged code
        public ReadableItemViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableItemViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public ReadableItemViewController() : base("ReadableItemView", NSBundle.MainBundle)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        #endregion

        //strongly typed view accessor
        public new ReadableItemView View
        {
            get
            {
                return (ReadableItemView)base.View;
            }
        }

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
                View.Readable = readable;
                DidChangeValue(nameof(Readable));
            }
        }
    }
}
