using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Readables.UI
{
    public class Installer: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IReadableDataStore>().ImplementedBy<ReadableDataStore>().LifestyleSingleton());
        }
    }
}
