using System;
using Readables.Common;

namespace Readables.Import.AggregatedEvents
{
    public class ImportFinishedAggregatedEvent : GenericAggregatedEvent<string>
    {
        public ImportFinishedAggregatedEvent(string eventObject) : base(eventObject)
        {
        }
    }
}
