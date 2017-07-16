using Readables.Common;
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
            var container = IOC.Container;
            var a  = container.Resolve<Import.IReadableImportService>();

            var readable = a.Import(@"C:\Users\Huszar Peter\Dropbox\Books\Neuromanc - William Gibson.epub");
            Console.WriteLine(readable.Title);
            base.OnStartup(e);
        }
    }
}
