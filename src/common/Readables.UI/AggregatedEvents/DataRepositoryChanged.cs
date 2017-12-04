using Readables.Common;

namespace Readables.UI.AggregatedEvents
{
    public class DataRepositoryChanged: IEvent
    {
        public DataRepositoryChangeReason Reason = DataRepositoryChangeReason.Import;
    }

    public enum DataRepositoryChangeReason
    {
        Filter,
        Import
    }
}
