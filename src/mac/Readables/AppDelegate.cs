using System;
using AppKit;
using Foundation;
using Readables.Common;
using System.IO;

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

			import.ImportFile(@"/Users/mac/Desktop/Amerikai istenek - Neil Gaiman.epub");
			import.ImportFile(@"/Users/mac/Desktop/Alapitvany - Isaac Asimov.epub");
			import.ImportFile(@"/Users/mac/Desktop/Abaddon kapuja - James S. A. Corey.epub");
			import.ImportFile(@"/Users/mac/Desktop/James S. A. Corey - A nemezis játékai.epub.epub");
            import.ImportFile(@"/Users/mac/Desktop/asd/Darth Maul.cbz");
		}

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
