using Readables.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readables.Import.FileFormat
{
    public interface IReadableImportService
    {
        string FormatName { get; }

        string[] SupportedExtensions { get; }

        Readable Import(string fileName);
    }
}
