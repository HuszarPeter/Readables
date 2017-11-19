using System;
using AppKit;
using Foundation;
using Readables.Domain;
using Readables.Extensions;

namespace Readables.ViewControllers.CoverView.Cells
{
    public partial class ReadableCoverItemView : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public ReadableCoverItemView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableCoverItemView(NSCoder coder) : base(coder)
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
                this.coverImage.Image = readable.CoverImage();
                DidChangeValue(nameof(Readable));
            }
        }

        bool selected;
        public bool IsSelected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                this.selectionBox.BorderColor = (selected) ? NSColor.AlternateSelectedControl : NSColor.Clear;
            }
        }

    }
}