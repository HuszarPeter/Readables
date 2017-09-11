using AppKit;
using Foundation;
using Readables.Common;

namespace Readables
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        public AppDelegate()
        {
            var container = IOC.Container;
            container.Install(new RootInstaller());
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
			// Insert code here to initialize your application
			var import = IOC.Container.Resolve<Import.IImportService>();
            import.ImportFolder(@"/Users/mac/Desktop");
            import.ImportFile(@"/Users/mac/Desktop/Amerikai istenek - Neil Gaiman.epub");
		}

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
