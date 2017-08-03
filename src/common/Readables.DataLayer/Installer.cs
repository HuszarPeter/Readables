using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Readables.DataLayer
{
    public class Installer : IWindsorInstaller
    {
        public Installer()
        {
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDataContext>().ImplementedBy<DataContext>());
		}
    }
}
