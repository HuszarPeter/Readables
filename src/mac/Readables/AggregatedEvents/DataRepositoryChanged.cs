using Readables.Common;

namespace Readables.AggregatedEvents
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
