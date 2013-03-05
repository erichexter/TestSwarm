using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.Collections.Generic;

namespace nTestSwarm.Application.Infrastructure.DomainEventing
{
    public interface IHandlerFactory
    {
        IEnumerable<IHandler<T>> GetAll<T>();
    }
}