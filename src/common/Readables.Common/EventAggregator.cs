using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Readables.Common
{
    public class EventAggregator : IEventAggregator
    {
        readonly ConcurrentQueue<AggregatedEvent> events = new ConcurrentQueue<AggregatedEvent>();

        readonly ConcurrentBag<Action<AggregatedEvent>> actions = new ConcurrentBag<Action<AggregatedEvent>>();

        public void Publish(AggregatedEvent eventObject)
        {
            this.events.Enqueue(eventObject);
            HandleLast();
        }

        public void Subscribe(Action<AggregatedEvent> eventHandler)
        {
            actions.Add(eventHandler);
        }

        private void HandleLast()
        {
            if (this.events.TryDequeue(out AggregatedEvent evt))
            {
                this.Handle(evt);
            }
        }

        private void Handle(AggregatedEvent eventObject)
        {
            foreach(var action in actions)
            {
                action(eventObject);
            }
        }
    }
}
