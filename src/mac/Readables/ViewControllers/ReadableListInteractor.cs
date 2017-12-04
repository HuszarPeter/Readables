using System;
using System.Collections.Generic;
using Readables.Common;
using Readables.Domain;
using Readables.UI;

namespace Readables.ViewControllers
{
    public class ReadableListInteractor
    {
        readonly IReadableDataStore dataRepository;

        public IEnumerable<Readable> Readables
        {
            get
            {
                return this.dataRepository.VisibleReadables;
            }
        }

        public ReadableListInteractor()
        {
            this.dataRepository = IOC.Resolve<IReadableDataStore>() ?? throw new NullReferenceException(nameof(IReadableDataStore));
        }
    }
}
