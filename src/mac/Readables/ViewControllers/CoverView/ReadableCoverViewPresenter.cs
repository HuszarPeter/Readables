using System;
using AppKit;
using Foundation;
using Readables.ViewControllers.CoverView.Cells;

namespace Readables.ViewControllers.CoverView
{
    public class ReadableCoverViewPresenter
    {
        private ReadableListInteractor interactor;

        public ReadableCoverViewPresenter()
        {
            this.interactor = new ReadableListInteractor();
        }

        internal int NumberOfItems()
        {
            return this.interactor.GetNumberofItems();
        }

        internal NSCollectionViewItem ItemAt(NSCollectionView collectionView, NSIndexPath indexPath)
        {
            var result = collectionView.MakeItem("coverItem", indexPath) as ReadableCoverItemViewController;
            result.Readable = this.interactor.ItemAt((int)indexPath.Item);
            return result;
        }
    }
}
