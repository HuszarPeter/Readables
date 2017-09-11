namespace Readables.Common
{
    public interface IEventAggregator {
		
        void SendMessage<T>(T message) where T : IEvent;

		void AddListener(IListenTo listener);
    }

    public interface IEvent {
        
    }

    public interface IListenTo {
        
    }

    public interface IListenTo<T>: IListenTo where T : IEvent {
        void HandleMessage(T message);
    }
}
