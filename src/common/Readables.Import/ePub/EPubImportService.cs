using Readables.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersFx.Formats.Text.Epub;

namespace Readables.Import.ePub
{
    public class EPubImportService : IReadableImportService
    {
        public Readable Import(string fileName)
        {
            var book = EpubReader.OpenBook(fileName);
            return new Readable()
            {
                Title = book.Title,
                Id = book.Schema.Package.Metadata.Identifiers.FirstOrDefault(m => m.Id == "uuid_id").Identifier,
                Files = new List<ReadableFile>
                {
                    new ReadableFile()
                    {
                        Language = book.Schema.Package.Metadata.Languages.FirstOrDefault(),
                        Location = fileName
                    }
                }
            };
        }
    }
}
