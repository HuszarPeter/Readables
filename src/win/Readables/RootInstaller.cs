using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Readables.Common;
using Readables.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readables
{
    public class RootInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Install(new Common.Installer())
                .Install(new DataLayer.Installer())
                .Install(new Import.Installer())
                .Install(new UI.Installer());

            container.Register(
                Classes.FromAssemblyInThisApplication()
                .BasedOn<IStartup>()
                .WithService.FromInterface()
                .LifestyleTransient()
            );

            //container.Register(
            //    Classes.FromThisAssembly()
            //    .BasedOn(typeof(IFileFormatImage<>))
            //    .WithService.FromInterface()
            //    .LifestyleTransient()
            //);

            //container.Register(
            //    Component.For<IDataRepository>()
            //    .ImplementedBy<DataRepository>()
            //    .LifestyleSingleton()
            //);

            var startup = container.ResolveAll<IStartup>();
            startup.ForEach(s => s.RunAtStartup());
        }
    }
}
