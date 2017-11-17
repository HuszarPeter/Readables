using System;
using System.Collections.Generic;
using Readables.Common;
using Readables.Data;
using Readables.Domain;

namespace Readables.ViewControllers
{
    public class ReadableListInteractor
    {
        readonly IDataRepository dataRepository;

        public IEnumerable<Readable> Readables
        {
            get
            {
                return this.dataRepository.VisibleReadables;
            }
        }

        public ReadableListInteractor()
        {
            this.dataRepository = IOC.Resolve<IDataRepository>() ?? throw new NullReferenceException(nameof(IDataRepository));
        }
    }
}
