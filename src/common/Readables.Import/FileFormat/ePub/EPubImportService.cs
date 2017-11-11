using Readables.Domain;
using System.Linq;
using VersFx.Formats.Text.Epub;
using System.Threading.Tasks;
using System;
using Readables.Import.Exceptions;

namespace Readables.Import.FileFormat.ePub
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
            try
            {
                var book = EpubReader.OpenBook(fileName);
                if (book == null)
                {
                    throw new ReadableImportException($"Can't read book: {fileName}");
                }

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
                        Location = fileName,
                        Format = "epub"
                    }
                },
                    Subjects = book.Schema.Package.Metadata.Subjects.ToArray(),
                    Description = book.Schema.Package.Metadata.Description,
                    Publisher = string.Join(", ", book.Schema.Package.Metadata.Publishers),
                    DateAdded = System.DateTime.Now
                };

                return result;
            }
            catch (ReadableImportException) 
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ReadableImportException("Epub reader exception", ex);
            }
        }
    }
}
