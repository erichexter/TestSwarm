namespace nTestSwarm.Application.Infrastructure.BusInfrastructure
{
    public interface IHandler<TMessage>
    {
        void Handle(TMessage message);
    }

    public interface IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        TResponse Handle(TRequest request);
    }
}