using System;
using Foundation;
using AppKit;
using Readables.ViewController;

namespace Readables
{
    public partial class BooksViewController : NSViewController
    {
        private BooksViewMode viewMode = BooksViewMode.All;
        public BooksViewMode ViewMode
        {
            get 
            {
                return viewMode;
            }
            set
            {
                viewMode = value;
                Console.WriteLine($"ViewMode has been set to {viewMode}");
            }
        }

		public BooksViewController (IntPtr handle) : base (handle)
		{
		}

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }
	}
}
