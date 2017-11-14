using System;
using AppKit;
using Foundation;

namespace Readables.ViewControllers.Outline
{
    public partial class ReadableOutlineDataCellView: NSTableCellView
    {
        public NSTextField BadgeText 
        { 
            get 
            { 
                return this.badgeText; 
            }
        }

        public override NSBackgroundStyle BackgroundStyle
        {
            get
            {
                return base.BackgroundStyle;
            }
            set
            {
                base.BackgroundStyle = value;
                this.UpdateView();
            }
        }


        // Called when created from unmanaged code
        public ReadableOutlineDataCellView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableOutlineDataCellView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        private void Initialize()
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.SetupView();
        }

        private void SetupView() 
        {
            NSLayoutConstraint.Create(
                this.badgeContainerView,
                NSLayoutAttribute.Width,
                NSLayoutRelation.GreaterThanOrEqual,
                this.badgeContainerView,
                NSLayoutAttribute.Height,
                1,
                0).Active = true;

            this.badgeContainerView.WantsLayer = true;
            this.UpdateView();   
        }

        private void UpdateView() {
            if (this.BackgroundStyle == NSBackgroundStyle.Light) 
            {
				this.badgeContainerView.Layer.BackgroundColor = NSColor.Knob.CGColor;
                this.badgeText.TextColor = NSColor.ControlLightHighlight;
            }
            else
            {
                this.badgeContainerView.Layer.BackgroundColor = NSColor.ControlLightHighlight.CGColor;
                this.badgeText.TextColor = NSColor.Knob;
            }
        }
    }
}
