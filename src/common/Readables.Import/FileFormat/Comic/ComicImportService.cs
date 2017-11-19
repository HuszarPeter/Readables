using System;
using System.Linq;
using System.IO;
using Ionic.Zip;
using Readables.Domain;
using SharpCompress.Reader;

namespace Readables.Import.FileFormat.Comic
{
    public class ComicImportService : IReadableImportService
    {
        public string FormatName => "Digital comic";

        public string[] SupportedExtensions => new[] { ".cbz", ".cbr" };

        public Readable Import(string fileName)
        {
            var fileInfo = new FileInfo(fileName);

            return new Readable
            {
                Title = fileInfo.Name,
                Author = "",
                Id = fileName,
                Files = new[] 
                {
                    new ReadableFile
                    {
                        Location = fileName,
                        Format = "comic"
                    }
                },
                Description = "",
                DateAdded = System.DateTime.Now,
                CoverImageBytes = CoverImage(fileName)
            };
        }

        private byte[] CoverImage(string fileName)
        {
            if (ZipFile.IsZipFile(fileName))
            {
                using (var zipFile = ZipFile.Read(fileName))
                {
                    var coverEntry = zipFile.Entries.FirstOrDefault(e => !e.IsDirectory);
                    using (var memoryStream = new MemoryStream())
                    {
                        coverEntry.Extract(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            }
            else
            {
                using (var fileStream = File.Open(fileName, FileMode.Open))
                {
                    var reader = ReaderFactory.Open(fileStream);
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            using (var ms = new MemoryStream())
                            {
                                reader.WriteEntryTo(ms);
                                return ms.ToArray();
                            }
                        }
                    }
                    return null;
                }
            }
        }
    }
}
