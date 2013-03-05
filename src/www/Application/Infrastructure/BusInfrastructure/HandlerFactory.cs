using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using StructureMap;
using System.Collections.Generic;

namespace nTestSwarm.Application.Infrastructure
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly IContainer _container;

        public HandlerFactory(IContainer container)
        {
            _container = container;
        }

        public IEnumerable<IHandler<T>> GetAll<T>()
        {
            return _container.GetAllInstances<IHandler<T>>();
        }

        public IHandler<TRequest, TResponse> Get<TRequest, TResponse>() where TRequest : IRequest<TResponse>
        {
            return _container.GetInstance<IHandler<TRequest, TResponse>>();
        }
    }
}