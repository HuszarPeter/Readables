using System;
using Readables.Common;
using Readables.Data;
using Readables.Domain;

namespace Readables.ViewControllers
{
    public class ReadableListInteractor
    {
        readonly IDataRepository dataRepository;

        public ReadableListInteractor()
        {
            this.dataRepository = IOC.Resolve<IDataRepository>() ?? throw new NullReferenceException(nameof(IDataRepository));
        }

        public int GetNumberofItems()
        {
            return this.dataRepository.NumberOfItemsInRepository();
        }

        public Readable ItemAt(int index)
        {
            return this.dataRepository.ItemAtPosition(index);
        }
    }
}
