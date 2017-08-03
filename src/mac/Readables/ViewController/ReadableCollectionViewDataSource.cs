using System;
using System.Linq;
using AppKit;
using Foundation;
using Readables.DataLayer;

namespace Readables.ViewController
{
    public class ReadableCollectionViewDataSource : AppKit.NSCollectionViewDataSource
    {
        readonly IDataContext dataContext;

        public ReadableCollectionViewDataSource(IDataContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));   
        }

        public override NSCollectionViewItem GetItem(NSCollectionView collectionView, NSIndexPath indexPath)
        {
            var result = collectionView.MakeItem("ReadableCell", indexPath) as ReadableItemViewController;
            result.Readable = this.dataContext.Readables.ElementAt((int)indexPath.Item);
            return result;
        }

        public override nint GetNumberofItems(NSCollectionView collectionView, nint section)
        {
            var result = this.dataContext.Readables.Count();
            return result;
        }
    }
}
