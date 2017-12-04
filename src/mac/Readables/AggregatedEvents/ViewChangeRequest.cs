using Readables.Common;
using Readables.UI.Model;

namespace Readables.AggregatedEvents
{
    public class ViewChangeRequest : IEvent
    {
        public ViewMode ViewMode;
    }
}
