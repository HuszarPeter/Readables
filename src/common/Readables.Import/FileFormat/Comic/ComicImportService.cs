using System;
using System.Linq;
using System.IO;
using Ionic.Zip;
using Readables.Domain;
using SharpCompress.Reader;
using System.Collections.Generic;
using Readables.Common.Extensions;

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
                DateAdded = DateTime.Now,
                CoverImageBytes = CoverImage(fileName)
            };
        }

        private byte[] CoverImage(string fileName)
        {
            if (ZipFile.IsZipFile(fileName))
            {
                return ReadFirstImageFromZipArchive(fileName);
            }
            else
            {
                return ReadFirstImageFromRarArchive(fileName);
            }
        }

        private byte[] ReadFirstImageFromZipArchive(string fileName)
        {
            using (var zipFile = ZipFile.Read(fileName))
            {
                var coverEntry = zipFile.Entries
                    .OrderBy(e => e.FileName)
                    .FirstOrDefault(e => !e.IsDirectory && e.FileName.IsPicture());
                using (var memoryStream = new MemoryStream())
                {
                    coverEntry.Extract(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        private byte[] ReadFirstImageFromRarArchive(string fileName)
        {
            using (var fileStream = File.Open(fileName, FileMode.Open))
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                var fileNames = new List<String>();
                using (var fileNameReader = ReaderFactory.Open(fileStream))
                {
                    while (fileNameReader.MoveToNextEntry())
                    {
                        if (!fileNameReader.Entry.IsDirectory)
                        {
                            fileNames.Add(fileNameReader.Entry.Key);
                        }
                    }
                }

                fileStream.Seek(0, SeekOrigin.Begin);
                using (var reader = ReaderFactory.Open(fileStream))
                {
                    while (reader.MoveToNextEntry())
                    {
                        if (reader.Entry.Key == fileNames.OrderBy(s => s).First(s => s.IsPicture()))
                        {
                            using (var ms = new MemoryStream())
                            {
                                reader.WriteEntryTo(ms);
                                return ms.ToArray();
                            }
                        }
                    }
                }
                return null;
            }
        }
    }
}
