using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using StructureMap;
using System.Collections.Generic;

namespace nTestSwarm.Application.Infrastructure.DomainEventing
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
    }
}