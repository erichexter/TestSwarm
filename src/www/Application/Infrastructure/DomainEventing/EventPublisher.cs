﻿using nTestSwarm.Application.Infrastructure.Threading;

namespace nTestSwarm.Application.Infrastructure.DomainEventing
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IHandlerFactory _handlerFactory;
        private readonly IThreadPool _threadPool;

        public EventPublisher(IHandlerFactory handlerFactory, IThreadPool threadPool)
        {
            _handlerFactory = handlerFactory;
            _threadPool = threadPool;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : class, IDomainEvent
        {
            _threadPool.Queue(() =>
            {
                var handlers = _handlerFactory.GetAll<TEvent>();

                handlers.Each(h => h.Handle(@event));
            });
        }
    }
}
