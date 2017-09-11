using System;
using Readables.Common;

namespace Readables.Import.AggregatedEvents
{
    public class FileImportedEvent: IEvent
    {
        public string FileName;

        public FileImportedEvent(String fileName)
        {
            this.FileName = fileName;
        }
    }
}
