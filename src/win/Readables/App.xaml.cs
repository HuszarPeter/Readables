using Readables.Common;
using Readables.Import;
using System;
using System.Windows;

namespace Readables
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IOC.Container.Install(new RootInstaller());

            var importService = IOC.Resolve<IImportService>();
            importService.ImportFile(@"C:\Users\Huszar Peter\Dropbox\Books\Neuromanc - William Gibson.epub");
            base.OnStartup(e);
        }
    }
}
