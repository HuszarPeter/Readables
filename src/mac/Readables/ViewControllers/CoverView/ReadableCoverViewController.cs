using System;
using AppKit;
using Foundation;
using Readables.ViewControllers.CoverView.Cells;

namespace Readables.ViewControllers.CoverView
{
    [Register(nameof(ReadableCoverViewController))]
    public partial class ReadableCoverViewController : NSViewController,
    INSCollectionViewDataSource,
    INSCollectionViewDelegate
    {
        private ReadableCoverViewPresenter presenter;

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
            this.presenter = new ReadableCoverViewPresenter();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            if (this.collectionView != null)
            {
                this.collectionView.RegisterClassForItem(typeof(ReadableCoverItemViewController), "coverItem");
			}
        }

        public nint GetNumberofItems(NSCollectionView collectionView, nint section)
        {
            return this.presenter.NumberOfItems();
        }

        public NSCollectionViewItem GetItem(NSCollectionView collectionView, NSIndexPath indexPath)
        {
            return this.presenter.ItemAt(collectionView, indexPath);
        }
    }
}
