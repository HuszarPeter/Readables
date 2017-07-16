using Readables.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readables.Import
{
    public interface IReadableImportService
    {
        Readable Import(string fileName);
    }
}
