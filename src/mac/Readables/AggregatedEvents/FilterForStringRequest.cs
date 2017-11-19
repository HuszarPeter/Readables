using System;
using Readables.Common;

namespace Readables.AggregatedEvents
{
    public class FilterForStringRequest: IEvent
    {
        public string FilterValue;
    }
}
