using System;
using Readables.Common;

namespace Readables.Import.AggregatedEvents
{
    public class FoldersImportRequestEvent: IEvent
    {
        public Uri[] Folders;
    }
}
