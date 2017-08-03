using Readables.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersFx.Formats.Text.Epub;
using Readables.DataLayer;

namespace Readables.Import.ePub
{
    public class EPubImportService : IReadableImportService
    {
        readonly IDataContext dataContext;

        public EPubImportService(IDataContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public Readable Import(string fileName)
        {
            var book = EpubReader.OpenBook(fileName);
            var result = new Readable()
            {
                Title = book.Title,
                Author = book.Author,
                Id = book.Schema.Package.Metadata.Identifiers.FirstOrDefault(m => m.Id == "uuid_id").Identifier,
                Files = new List<ReadableFile>
                {
                    new ReadableFile()
                    {
                        Language = book.Schema.Package.Metadata.Languages.FirstOrDefault(),
                        Location = fileName
                    }
                },
                Subjects = book.Schema.Package.Metadata.Subjects,
                Description = book.Schema.Package.Metadata.Description
            };

            this.dataContext.Upsert(result);

            return result;
        }
    }
}
