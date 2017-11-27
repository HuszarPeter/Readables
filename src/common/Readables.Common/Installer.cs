using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Readables.Common
{
    public class Installer: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton());
        }
    }
}
