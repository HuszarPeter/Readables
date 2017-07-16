using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readables.Common
{
    public static class IOC
    {
        public static IWindsorContainer Container { get; private set; }

        static IOC()
        {
            Container = new WindsorContainer()
                    .Install(new Import.Installer());
        }
    }
}
