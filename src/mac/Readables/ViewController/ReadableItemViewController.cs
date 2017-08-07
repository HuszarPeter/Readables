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

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            UpdateView();
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

        public override bool Selected
        {
            get
            {
                return base.Selected;
            }
            set
            {
                base.Selected = value;
                UpdateView();
            }
        }

        void UpdateView()
        {
            selectionBorder.AlphaValue = (nfloat)((Selected) ? 0.6 : 1.0);
            //selectionBorder.BorderColor = NSColor.Clear;
        }
    }
}
