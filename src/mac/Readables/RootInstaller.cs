using System;
using System.Linq;
using AppKit;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Foundation;
using Readables.Common;
using Readables.Common.Extensions;

namespace Readables
{
    public class RootInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Install(new Common.Installer())
                .Install(new DataLayer.Installer())
                .Install(new Import.Installer());

            container.Register(
                Classes.FromAssemblyInThisApplication()
                .BasedOn<IStartup>()
                .WithService.FromInterface()
                .LifestyleTransient()
            );

            container.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(IFileFormatImage<>))
                .WithService.FromInterface()
                .LifestyleTransient()
            );

            var startup = container.ResolveAll<IStartup>();
            startup.ForEach(s => s.RunAtStartup());
        }
    }

    // TODO: Move the interface to Import
    public interface IFileFormatImage<T> {
        string FileExtension { get; }
        T Image { get; }
    }

    // TODO: Move these implementations to somewhere else...
    public class EpubFileFormatImage : IFileFormatImage<NSImage>
    {
        public string FileExtension => "epub";

        public NSImage Image => NSBundle.MainBundle.ImageForResource("format_epub");
    }

    public class ComicFileFormatImage : IFileFormatImage<NSImage>
    {
        public string FileExtension => "cbz";

        public NSImage Image => NSBundle.MainBundle.ImageForResource("format_comic");
    }
}
