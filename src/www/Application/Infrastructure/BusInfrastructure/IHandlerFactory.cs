using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.Collections.Generic;

namespace nTestSwarm.Application.Infrastructure
{
    public interface IHandlerFactory
    {
        IEnumerable<IHandler<T>> GetAll<T>();
        IHandler<TRequest, TResponse> Get<TRequest, TResponse>() where TRequest : IRequest<TResponse>;
    }
}