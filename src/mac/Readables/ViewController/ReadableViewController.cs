using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Readables.DataLayer;
using Readables.Common;

namespace Readables.ViewController
{
    public partial class ReadableViewController : AppKit.NSViewController
    {
        private IReadableRepository readableRepository;

        #region Constructors

        // Called when created from unmanaged code
        public ReadableViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public ReadableViewController() : base("ReadableView", NSBundle.MainBundle)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
            this.readableRepository = IOC.Container.Resolve<IReadableRepository>();
        }

        #endregion
        //strongly typed view accessor
        public new ReadableView View
        {
            get
            {
                return (ReadableView)base.View;
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

			this.collectionView.WantsLayer = true;
            this.collectionView.RegisterClassForItem(typeof(ReadableItemViewController), "ReadableCell");
            this.collectionView.DataSource = new ReadableCollectionViewDataSource(this.readableRepository);
            this.collectionView.ReloadData();
        }
    }
}
