using System;
using Foundation;
using Readables.DataLayer;
using Readables.Common;
using Readables.Import.AggregatedEvents;

namespace Readables.ViewController.List
{
    public partial class ReadableListViewController : AppKit.NSViewController, IListenTo<FileImportedEvent>, IListenTo<PathImportedEvent>
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
            this.eventAggregator.AddListener(this);
        }


        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.readablesTableView.DataSource = new ReadableListViewDataSource(this.dataContext);
            this.refrestData();
		}

		public void HandleMessage(PathImportedEvent message)
		{
            Console.WriteLine($"Imported: {message.NumberOfSuccessfullyImported}. Failed: {message.NumberOfFailedImport}");
            this.refrestData();
		}
		
        public void HandleMessage(FileImportedEvent message)
		{
            Console.WriteLine($"File imported {message.FileName}");
            this.refrestData();
        }

		private void refrestData() {
            this.readablesTableView.ReloadData();
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
