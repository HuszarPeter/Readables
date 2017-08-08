using System;
namespace Readables.Import
{
    public interface IImportService
    {
        void ImportFile(string fileName);

        void ImportFolder(string path);
    }
}
