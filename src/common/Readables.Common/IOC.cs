using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;

namespace Readables.Common
{
    public static class IOC
    {
        public static IWindsorContainer Container { get; private set; }

        static IOC()
        {
            Container = new WindsorContainer();
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel, true));
			Container.Kernel.Resolver.AddSubResolver(new ArrayResolver(Container.Kernel, true));
		}

        public static T Resolve<T>() {
            return Container.Resolve<T>();
        }

        public static IEnumerable<T> ResolveAll<T>() {
            return Container.ResolveAll<T>();
        }
    }
}
