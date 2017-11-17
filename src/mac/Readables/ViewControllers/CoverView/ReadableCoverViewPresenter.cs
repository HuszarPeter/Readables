using AppKit;
using Foundation;
using Readables.Domain;
using Readables.ViewControllers.CoverView.Cells;

namespace Readables.ViewControllers.CoverView
{
    public class ReadableCoverViewPresenter
    {
        internal NSCollectionViewItem PresentItemView(NSCollectionView collectionView, Readable readable, NSIndexPath index)
        {
            var result = collectionView.MakeItem("coverItem", index) as ReadableCoverItemViewController;
            result.Readable = readable;
            return result;
        }
    }
}
