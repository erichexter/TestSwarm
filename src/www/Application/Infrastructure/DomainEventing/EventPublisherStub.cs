namespace nTestSwarm.Application.Infrastructure.DomainEventing
{
    public class EventPublisherStub : IEventPublisher
    {
        public void Publish<TEvent>(TEvent @event) where TEvent : class, IDomainEvent
        {
            
        }
    }
}