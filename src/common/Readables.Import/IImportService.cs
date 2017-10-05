namespace Readables.Import
{
    public interface IImportService
    {
        bool CanImportFile(string fileName);

        void ImportFile(string fileName);

        void ImportFolder(string path);

        void ImportPath(string folderOrFile);
    }
}
