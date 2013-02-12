namespace nTestSwarm.Application.Infrastructure.DomainEventing
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : class, IDomainEvent;
    }
}