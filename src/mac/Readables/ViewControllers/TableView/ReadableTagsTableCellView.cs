using System;
using System.Linq;
using AppKit;
using Foundation;
using Readables.Domain;
using Readables.Extensions;

namespace Readables.ViewControllers.TableView
{
    public class ReadableTagsTableCellView: NSTableCellView
    {
        private NSStackView stackView;
        private Readable readable;
     
        public Readable Readable
        {
            get
            {
                return readable;
            }
            set
            {
                this.readable = value;
                this.UpdateUI();
            }
        }

        public ReadableTagsTableCellView()
        {
            this.SetupUI();
        }

        private void SetupUI()
        {
            foreach (var subView in this.Subviews.Clone() as NSView[])
            {
                subView.RemoveFromSuperview();
            }

            this.stackView = new NSStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Orientation = NSUserInterfaceLayoutOrientation.Horizontal,
                Spacing = 1,
                EdgeInsets = new NSEdgeInsets(0,0,0,0),
                Distribution = NSStackViewDistribution.GravityAreas,
            };
            this.AddSubview(this.stackView);

            NSLayoutConstraint.FromVisualFormat("H:|-(0)-[stack]-(0)-|", NSLayoutFormatOptions.None, "stack", this.stackView)
                              .ActivateAll();
            NSLayoutConstraint.FromVisualFormat("V:|-(0)-[stack]-(0)-|", NSLayoutFormatOptions.None, "stack", this.stackView)
                              .ActivateAll();

        }

        private void UpdateUI()
        {
            if (this.Readable == null)
            {
                return;
            }

            var imageViews = this.Readable.Files
                                 .Select(file => file.Format)
                                 .Select(format => 
                                    {
                                        return new NSImageView
                                        {
                                            Image = NSBundle.MainBundle.ImageForResource(FileFormatToImageName(format)),
                                            ImageAlignment = NSImageAlignment.Left,
                                            ImageScaling = NSImageScale.None
                                        };
                                    })
                                    .ToArray();
            this.stackView.SetViews(imageViews, NSStackViewGravity.Leading);
        }
        private string FileFormatToImageName(string format) {
            switch (format)
            {
                case "epub":
                    return "format_epub";
                case "mobi":
                    return "format_azw3";
                case "comic":
                    return "format_comic";
                default:
                    break;
            }
            return "format_epub";
        }
    }
}
