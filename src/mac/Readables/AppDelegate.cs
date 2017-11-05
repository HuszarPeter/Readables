using AppKit;
using Foundation;
using Readables.Common;
using Readables.Import.Metadata.Moly;

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

            //var s = new MolyMetadataScraperService();
            //var ss = s.ScrapeMetadataAsync(new Domain.Readable(){ Title = "Babilon hamvai"});
            //System.Console.WriteLine(ss);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
