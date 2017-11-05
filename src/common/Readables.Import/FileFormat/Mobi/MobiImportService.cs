using Readables.Domain;

namespace Readables.Import.FileFormat.Mobi
{
    public class MobiImportService: IReadableImportService
    {
        public string FormatName => "Mobi";

        public string[] SupportedExtensions => new[] { ".mobi", ".azw3" };

        public Readable Import(string fileName)
        {
            var metadataReader = new MobiMetadataReader.Net.Metadata.MobiMetadata(fileName);
            var result = new Readable
            {
                Title = metadataReader.MobiHeader.EXTHHeader.UpdatedTitle,
                Author = metadataReader.MobiHeader.EXTHHeader.Author,
                Files = new[]{
                    new ReadableFile {
                        Language = "hu",
                        Location = fileName
                    }
                },
                Description = metadataReader.MobiHeader.EXTHHeader.Description,
                Subjects = new[] { metadataReader.MobiHeader.EXTHHeader.Subject },
                Id = metadataReader.MobiHeader.EXTHHeader.ASIN
            };
            return result;
        }
    }
}
