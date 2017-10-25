using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Readables.Import
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IImportService>().ImplementedBy<ImportService>().LifestyleTransient());
            container.Register(Component.For<IReadableImportService>().ImplementedBy<ePub.EPubImportService>().LifestyleTransient());
            container.Register(Component.For<IReadableImportService>().ImplementedBy<Comic.ComicImportService>().LifestyleTransient());
            container.Register(Component.For<IReadableImportService>().ImplementedBy<Mobi.MobiImportService>().LifestyleTransient());
        }
    }
}
