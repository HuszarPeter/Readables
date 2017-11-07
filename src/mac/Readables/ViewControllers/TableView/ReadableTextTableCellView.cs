using System;
using System.Linq;
using AppKit;
using Readables.Domain;
using Readables.Extensions;

namespace Readables.ViewControllers.TableView
{
    public class ReadableTextTableCellView : NSTableCellView
    {
        private NSTextField textField;
        private Readable readable;
        private ReadableFieldView field = ReadableFieldView.Title;

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

        public ReadableFieldView Field {
            get
            {
                return this.field;
            }
            set
            {
                this.field = value;
                this.UpdateUI();
            }
        }

        public ReadableTextTableCellView()
        {
            this.SetupView();    
        }

        private void SetupView() 
        {
            foreach(var subView in this.Subviews.Clone() as NSView[]) {
                subView.RemoveFromSuperview();
            }

            this.textField = new NSTextField
            {
                StringValue = "",
                Bordered = false,
                BackgroundColor = NSColor.Clear,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Alignment = NSTextAlignment.Natural,
                LineBreakMode = NSLineBreakMode.TruncatingTail
            };
            this.AddSubview(this.textField);

            NSLayoutConstraint.FromVisualFormat("H:|-(0)-[label]-(0)-|", NSLayoutFormatOptions.None, "label", this.textField)
                              .ActivateAll();
            NSLayoutConstraint.FromVisualFormat("V:|-(0)-[label]-(0)-|", NSLayoutFormatOptions.None, "label", this.textField)
                              .ActivateAll();
        }

        private void UpdateUI() {
            if (this.readable == null)
            {
                return;
            }

            switch(this.Field)
            {
                case ReadableFieldView.Title:
                    {
                        this.textField.StringValue = this.readable.Title;
                        break;
                    }
                case ReadableFieldView.Author:
                    {
                        this.textField.StringValue = this.readable.Author;
                        break;
                    }
                case ReadableFieldView.DateAdded:
                    {
                        this.textField.StringValue = $"{this.readable.DateAdded}";
                        break;
                    }
                case ReadableFieldView.Formats:
                    {
                        this.textField.StringValue = String.Join(", ", this.readable.Files.Select(f => f.Format));
                        break;
                    }
                default:
                    {
                        this.textField.StringValue = "??";
                        break;
                    }
            }
        }
    }

    public enum ReadableFieldView {
        Title,
        Author,
        DateAdded,
        Formats
    }
}
