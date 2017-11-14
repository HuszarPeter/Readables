using System;

namespace Readables.Import
{
    public interface IFileFormatImageManager
    {
        void RegisterTagImageForFileFormat(string extension, Object image);
    }
}
