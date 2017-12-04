using System;
using Readables.Common;

namespace Readables.UI.AggregatedEvents
{
    public class FilterForStringRequest: IEvent
    {
        public string FilterValue;
    }
}
