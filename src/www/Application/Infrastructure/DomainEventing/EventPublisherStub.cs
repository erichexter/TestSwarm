using Microsoft.AspNet.SignalR;
using nTestSwarm.Application.Events.JobCompletion;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Infrastructure.Threading;
using nTestSwarm.Application.Queries.JobStatus;
using nTestSwarm.Application.Queries.LatestJobForProgram;
using nTestSwarm.Hubs;

namespace nTestSwarm.Application.Infrastructure.DomainEventing
{
    public class EventPublisherStub : IEventPublisher
    {
        private readonly IThreadPool _threadPool;
        private readonly IBus _bus;

        public EventPublisherStub(IThreadPool threadPool, IBus bus)
        {
            _threadPool = threadPool;
            _bus = bus;
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
                var e = (RunCompleted)(object)@event;
                var status = _bus.Request(new JobStatusQuery(e.JobId)).Data;
                
                JobStatusHub.UpdateStatus(status);
            }
        }
    }
}