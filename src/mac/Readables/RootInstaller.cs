using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

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
        }
    }
}
