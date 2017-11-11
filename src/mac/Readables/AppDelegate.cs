using AppKit;
using Foundation;
using Readables.AggregatedEvents;
using Readables.Common;

namespace Readables
{
    [Register("AppDelegate")]
    public partial class AppDelegate : NSApplicationDelegate, 
        IListenTo<MenuImportFileRequest>, 
        IListenTo<MenuImportFileRequestCompleted>,
        IListenTo<MenuImportDirectoryRequest>,
        IListenTo<MenuImportDirectoryRequestCompleted>
    {
        private IEventAggregator eventAggregator;

        public AppDelegate()
        {
            var container = IOC.Container;
            container.Install(new RootInstaller());

            this.eventAggregator = IOC.Container.Resolve<IEventAggregator>();
            this.eventAggregator.AddListener(this);
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }

        partial void fileImportMenuAction(AppKit.NSMenuItem sender)
        {
            this.eventAggregator.SendMessage(new MenuImportFileRequest());
        }

        partial void folderImportMenuAction(AppKit.NSMenuItem sender)
        {
            this.eventAggregator.SendMessage(new MenuImportDirectoryRequest());
        }


        #region Event Handlers
        public void HandleMessage(MenuImportFileRequest message)
        {
            this.fileImportMenuItem.Enabled = false;
        }

        public void HandleMessage(MenuImportFileRequestCompleted message)
        {
            this.fileImportMenuItem.Enabled = true;
        }

        public void HandleMessage(MenuImportDirectoryRequest message)
        {
            this.directoryImportMenuItem.Enabled = false;
        }

        public void HandleMessage(MenuImportDirectoryRequestCompleted message)
        {
            this.directoryImportMenuItem.Enabled = true;
        }
        #endregion
    }
}
