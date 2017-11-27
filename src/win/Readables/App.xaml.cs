using NLog;
using Readables.Common;
using Readables.Import;
using Readables.Import.AggregatedEvents;
using System;
using System.Windows;

namespace Readables
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            logger.Info("Application started");

            IOC.Container.Install(new RootInstaller());
            var evtAggregator = IOC.Resolve<IEventAggregator>();
            evtAggregator.SendMessage(new FilesImportRequestEvent
            {
                Files = new[] {
                    new Uri(@"C:\Users\Huszar Peter\Dropbox\Books\Neuromanc - William Gibson.epub")
                }
            });

            base.OnStartup(e);
        }
    }
}
