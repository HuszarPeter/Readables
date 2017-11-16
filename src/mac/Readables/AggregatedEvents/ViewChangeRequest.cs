using System;
using Readables.Common;

namespace Readables.AggregatedEvents
{
    public class ViewChangeRequest : IEvent
    {
        public ViewMode ViewMode;
    }

    public enum ViewMode
    {
        List,
        Collection,
        ListWithDetails
    }

    public static class ViewModeExtensions
    {
        public static nint AsNint(this ViewMode viewMode)
        {
            switch (viewMode)
            {
                case ViewMode.List:
                    return 0;
                case ViewMode.Collection:
                    return 1;
                case ViewMode.ListWithDetails:
                    return 2;
            }

            throw new Exception("Unknown mode");
        }

        public static ViewMode AsViewMode(this nint value)
        {
            switch (value)
            {
                case 0:
                    return ViewMode.List;
                case 1:
                    return ViewMode.Collection;
                case 2:
                    return ViewMode.ListWithDetails;
            }

			throw new Exception("Unknown mode");
        }
    }
}
