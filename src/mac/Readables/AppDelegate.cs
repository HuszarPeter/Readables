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
            var import = IOC.Container.Resolve<Readables.Import.IReadableImportService>();
			var readable = import.Import(@"/Users/mac/Desktop/Amerikai istenek - Neil Gaiman.epub");
            System.Console.WriteLine($"{readable.Title} - {readable.Author} - {readable.Id}");
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
