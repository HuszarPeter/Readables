using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Readables.Import
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IReadableImportService>().ImplementedBy<ePub.EPubImportService>());
        }
    }
}
