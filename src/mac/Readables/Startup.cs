using System;
using Foundation;
using Readables.Common;
using Readables.Import;

namespace Readables
{
    public class Startup: IStartup
    {
        readonly IFileFormatImageManager fileFormatManager;

        public Startup(IFileFormatImageManager formatManager)
        {
            this.fileFormatManager = formatManager ?? throw new ArgumentNullException(nameof(formatManager));
        }

        public void RunAtStartup()
        {
            //Console.WriteLine($"Run at startup : {this.GetType()}");

            //this.fileFormatManager.RegisterTagImageForFileFormat(".epub", NSBundle.MainBundle.ImageForResource("format_epub"));
        }
    }
}
