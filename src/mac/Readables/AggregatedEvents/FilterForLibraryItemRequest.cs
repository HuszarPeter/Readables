using System;
using Readables.Common;

namespace Readables.AggregatedEvents
{
    public class FilterForLibraryItemRequest: IEvent
    {
        public string Format;
    }
}
