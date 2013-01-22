using System;

namespace nTestSwarm.Application.Infrastructure.BusInfrastructure
{
    public interface IBus
    {
        Result<TResponse> Request<TResponse>(IRequest<TResponse> request);
        Result Send<TMessage>(TMessage message);
        void AsyncRequest<TResponse>(IRequest<TResponse> request, Action<Result<TResponse>> onCompleted);
    }
}