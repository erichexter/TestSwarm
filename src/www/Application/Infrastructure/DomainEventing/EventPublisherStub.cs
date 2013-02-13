using Microsoft.AspNet.SignalR;
using nTestSwarm.Application.Events.JobCompletion;
using nTestSwarm.Application.Infrastructure.Threading;
using nTestSwarm.Hubs;

namespace nTestSwarm.Application.Infrastructure.DomainEventing
{
    public class EventPublisherStub : IEventPublisher
    {
        private readonly IThreadPool _threadPool;

        public EventPublisherStub(IThreadPool threadPool)
        {
            _threadPool = threadPool;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : class, IDomainEvent
        {
            _threadPool.Queue(() => HandleEvent(@event));
        }

        // temporary impl
        private void HandleEvent<TEvent>(TEvent @event) where TEvent : class, IDomainEvent
        {
            if (@event is RunCompleted)
            {
                GlobalHost.ConnectionManager.GetHubContext<LastJobStatusHub>().Clients.All.statusUpdated(@event);
            }
        }
    }
}