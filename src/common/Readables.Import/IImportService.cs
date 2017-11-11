using System;

namespace Readables.Import
{
    public interface IImportService
    {
        bool CanImportFile(string fileName);

        void ImportFile(string fileName);

        void ImportFile(Uri file);

        void ImportFolder(string path);

        void ImportFolder(Uri folder);

        void ImportPath(string folderOrFile);
    }
}
