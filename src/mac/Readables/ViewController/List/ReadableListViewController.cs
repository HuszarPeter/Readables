using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Readables.DataLayer;
using Readables.Common;

namespace Readables.ViewController.List
{
    public partial class ReadableListViewController : AppKit.NSViewController
    {
        private IDataContext dataContext;

        private IEventAggregator eventAggregator;

        #region Constructors

        // Called when created from unmanaged code
        public ReadableListViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ReadableListViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public ReadableListViewController() : base("ReadableListView", NSBundle.MainBundle)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
            this.dataContext = IOC.Container.Resolve<IDataContext>();
            this.eventAggregator = IOC.Container.Resolve<IEventAggregator>();
            this.eventAggregator.Subscribe(item => readablesTableView.ReloadData());
        }


        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.readablesTableView.DataSource = new ReadableListViewDataSource(this.dataContext);
        }

        #endregion

        //strongly typed view accessor
        public new ReadableListView View
        {
            get
            {
                return (ReadableListView)base.View;
            }
        }
    }
}
