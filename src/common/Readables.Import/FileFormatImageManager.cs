using System;
using System.Collections.Generic;

namespace Readables.Import
{
    public class FileFormatImageManager: IFileFormatImageManager
    {
        private readonly Dictionary<string, Object> registrations = new Dictionary<string, object>();

        public void RegisterTagImageForFileFormat(string extension, Object image)
        {
            registrations[extension] = image;
        }
    }
}
