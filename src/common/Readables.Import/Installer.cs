using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Readables.Import.FileFormat;
using Readables.Import.Metadata;

namespace Readables.Import
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<StartableFacility>();

            container.Register(Component.For<IImportService>().ImplementedBy<ImportService>().LifestyleTransient());

            container.Register(
                Classes.FromThisAssembly()
                .BasedOn<IReadableImportService>()
                .WithService.FromInterface()
                .LifestyleTransient());

            container.Register(
                Classes.FromThisAssembly()
                .BasedOn<IMetadataScraperService>()
                .WithService.FromInterface()
                .LifestyleTransient());

            container.Register(
                Component.For<IFileFormatImageManager>().ImplementedBy<FileFormatImageManager>().LifestyleSingleton()
            );

            container.Register(Component.For<StartableImportComponent>().Start());
        }
    }
}
