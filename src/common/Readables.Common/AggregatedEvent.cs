using System;
namespace Readables.Common
{
    public abstract class AggregatedEvent
    {
    }

    public class GenericAggregatedEvent<T> : AggregatedEvent 
    {
        public T EventObject { get; set; }

        public GenericAggregatedEvent(T eventObject)
        {
            this.EventObject = eventObject;
        }
    }
}
