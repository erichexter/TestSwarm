using nTestSwarm.Application.Events.JobCompletion;
using nTestSwarm.Application.Infrastructure.Threading;

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

        private void HandleEvent<TEvent>(TEvent @event) where TEvent : class, IDomainEvent
        {
            if (@event is RunCompleted)
            {

            }
        }
    }
}