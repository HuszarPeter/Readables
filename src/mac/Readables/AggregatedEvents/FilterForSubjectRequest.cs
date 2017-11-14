using System;
using Readables.Common;

namespace Readables.AggregatedEvents
{
    public class FilterForSubjectRequest: IEvent
    {
        public string Subject;
    }
}
