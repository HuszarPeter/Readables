using System;
using System.Linq;
using AppKit;
using Foundation;
using Readables.AggregatedEvents;
using Readables.Common;
using Readables.ViewControllers.CoverView.Cells;

namespace Readables.ViewControllers.CoverView
{
    [Register(nameof(ReadableCoverViewController))]
    public partial class ReadableCoverViewController : 
    NSViewController,
    INSCollectionViewDataSource,
    INSCollectionViewDelegate,
    IEventAggregatorSubscriber,
    IListenTo<DataRepositoryChanged>
    {
        private IEventAggregator eventAggregator;
        private ReadableCoverViewPresenter presenter;
        private ReadableListInteractor interactor;

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
            this.interactor = new ReadableListInteractor();
            this.eventAggregator = IOC.Resolve<IEventAggregator>();
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
            return this.interactor.Readables.Count();
        }

        public NSCollectionViewItem GetItem(NSCollectionView collectionView, NSIndexPath indexPath)
        {
            var readable = this.interactor.Readables.ElementAt((int)indexPath.Item);
            return this.presenter.PresentItemView(collectionView, readable, indexPath);
        }

        public void HandleMessage(DataRepositoryChanged message)
        {
            this.collectionView.ReloadData();
        }

        public void SubscribeToAggregatedEvents()
        {
            this.eventAggregator.AddListener(this);
        }

        public void UnSubscribeFromAggregatedEvents()
        {
            this.eventAggregator.RemoveListener(this);
        }
    }
}
