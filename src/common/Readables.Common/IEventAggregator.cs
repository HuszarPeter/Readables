using System;
namespace Readables.Common
{
    public interface IEventAggregator
    {
        void Publish(AggregatedEvent eventObject);

        void Subscribe(Action<AggregatedEvent> eventHandler);
    }
}
