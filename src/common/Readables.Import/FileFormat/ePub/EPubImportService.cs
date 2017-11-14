using System;
using System.Linq;
using Readables.Domain;
using Readables.Import.Exceptions;
using VersFx.Formats.Text.Epub;
using VersFx.Formats.Text.Epub.Schema.Opf;

namespace Readables.Import.FileFormat.ePub
{
    public class EPubImportService : IReadableImportService
    {
        public string FormatName => "ePub";

        public string[] SupportedExtensions => new[] { ".epub" };

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
                    DateAdded = System.DateTime.Now,
                    Series = book.Schema.Package.Metadata.GetSeries(),
                    SeriesIndex = book.Schema.Package.Metadata.GetSeriesIndex()
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

    public static class EpubMetadataExtensions {
        public static string GetSeries(this EpubMetadata meta) {
            var item = meta.MetaItems.FirstOrDefault(m => m.Name == "calibre:series");
            return item != null ? item.Content : "";
        }

        public static string GetSeriesIndex(this EpubMetadata meta) {
            var item = meta.MetaItems.FirstOrDefault(m => m.Name == "calibre:series_index");
            var result = item != null ? item.Content : "";
            if(double.TryParse(result, out double x))
            {
                return $"{(int)x}";
            }
            return result;
        }
    }
}
