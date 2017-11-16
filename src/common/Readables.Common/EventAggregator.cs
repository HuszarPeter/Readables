using System.Collections.Generic;
using Readables.Common.Extensions;

namespace Readables.Common
{
	public class EventAggregator : IEventAggregator
	{
        private readonly List<IListenTo> listeners = new List<IListenTo>();

        public void AddListener(IListenTo listener)
		{
			this.listeners.Add(listener);
		}

        public void RemoveListener(IListenTo listener)
        {
            if (this.listeners.Contains(listener))
            {
                this.listeners.Remove(listener);
            }
        }

        public void SendMessage<T>(T message) where T : IEvent
		{
            IListenTo[] copy = new IListenTo[this.listeners.Count];
            this.listeners.CopyTo(copy);

			copy.CallOnEach<IListenTo<T>>((obj) => {
				obj.HandleMessage(message);
			});
		}
	}
}
