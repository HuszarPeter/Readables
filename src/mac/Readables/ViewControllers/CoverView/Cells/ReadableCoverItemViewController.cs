using System;
using Foundation;
using AppKit;
using Readables.Domain;

namespace Readables.ViewControllers.CoverView.Cells
{
    public partial class ReadableCoverItemViewController : NSCollectionViewItem
    {
        #region Constructors and initializers
        // Called when created from unmanaged code
        public ReadableCoverItemViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableCoverItemViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public ReadableCoverItemViewController() : base("ReadableCoverItemView", NSBundle.MainBundle)
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

        //strongly typed view accessor
        public new ReadableCoverItemView View
        {
            get
            {
                return (ReadableCoverItemView)base.View;
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
                View.IsSelected = value;
            }
        }
    }
}
