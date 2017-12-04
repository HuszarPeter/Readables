using Readables.Common;
using Readables.DataLayer;
using Readables.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readables.ViewModel.List
{
    public class ListViewModel: BaseViewModel
    {
        private readonly IReadableRepository readableRepository;

        public IEnumerable<Readable> Readables
        {
            get
            {
                return readableRepository.GetAllReadables();
            }
        }

        public ListViewModel()
        {
            this.readableRepository = IOC.Resolve<IReadableRepository>();
        }
    }
}
