using System;
using Readables.Domain;

namespace Readables.Import.Metadata
{
    public interface IMetadataScraperService
    {
        ReadableMetadata ScrapeMetadataAsync(Readable readable);
    }
}
