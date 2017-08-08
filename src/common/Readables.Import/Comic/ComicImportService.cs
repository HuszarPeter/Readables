﻿using System;
using System.IO;
using Readables.Domain;

namespace Readables.Import.Comic
{
    public class ComicImportService : IReadableImportService
    {
        public string FormatName => "Digital comic";

        public string[] SupportedExtensions => new[] { ".cbz", ".cbr" };

        public Readable Import(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return new Readable()
            {
                Title = fileInfo.Name,
                Author = "",
                Id = fileName,
                Files = new[] 
                {
                    new ReadableFile
                    {
                        Location = fileName
                    }
                },
                Description = "",
            };
        }
    }
}
