using System;
using Readables.Common;

namespace Readables.Import.AggregatedEvents
{
    public class PathImportedEvent: IEvent
    {
        public int NumberOfSuccessfullyImported;

        public int NumberOfFailedImport;

    }
}
