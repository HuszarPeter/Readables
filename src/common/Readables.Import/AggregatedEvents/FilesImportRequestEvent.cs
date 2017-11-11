using System;
using Readables.Common;

namespace Readables.Import.AggregatedEvents
{
    public class FilesImportRequestEvent: IEvent
    {
        public Uri[] Files;
    }
}
