using Readables.Domain;
using System.Linq;
using VersFx.Formats.Text.Epub;

namespace Readables.Import.ePub
{
    public class EPubImportService : IReadableImportService
    {
		public string FormatName 
        {
            get => "ePub";
        }

		public string[] SupportedExtensions 
        {
            get => new[] { ".epub" };
        }

        public Readable Import(string fileName)
        {
            var book = EpubReader.OpenBook(fileName);
            var result = new Readable
            {
                Title = book.Title,
                Author = book.Author,
                Id = book.Schema.Package.Metadata.Identifiers.FirstOrDefault(m => m.Id == "uuid_id").Identifier,
                Files = new[]
                {
                    new ReadableFile
                    {
                        Language = book.Schema.Package.Metadata.Languages.FirstOrDefault(),
                        Location = fileName
                    }
                },
                Subjects = book.Schema.Package.Metadata.Subjects.ToArray(),
                Description = book.Schema.Package.Metadata.Description
            };

            return result;
        }
    }
}
