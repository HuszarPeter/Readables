using System;
using System.Collections.Generic;
using System.Linq;
using AppKit;
using Foundation;
using Readables.DataLayer;
using Readables.Domain;

namespace Readables.ViewController
{
    public class ReadableCollectionViewDataSource : AppKit.NSCollectionViewDataSource
    {
        readonly IReadableRepository readableRepository;
        private List<Readable> allReadables;

        public ReadableCollectionViewDataSource(IReadableRepository readableRepository)
        {
            this.readableRepository = readableRepository ?? throw new ArgumentNullException(nameof(readableRepository));   
        }

        public override NSCollectionViewItem GetItem(NSCollectionView collectionView, NSIndexPath indexPath)
        {
            var result = collectionView.MakeItem("ReadableCell", indexPath) as ReadableItemViewController;
            result.Readable = this.allReadables.ElementAt((int)indexPath.Item);
            return result;
        }

        public override nint GetNumberofItems(NSCollectionView collectionView, nint section)
        {
            this.allReadables = this.readableRepository.GetAllReadables().ToList();
            return this.allReadables.Count();
        }
    }
}
