using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.Net;
using System.Web.Http;

namespace nTestSwarm.Api
{
    public abstract class BusApiController : ApiController
    {
        private readonly IBus _bus;

        public BusApiController(IBus bus)
        {
            _bus = bus;
        }

        protected TResponse Query<TResponse>(IRequest<TResponse> request)
        {
            var result = _bus.Request(request);

            if (result.HasException)
                //TODO: transform exception and log
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            else
                return result.Data;
        }

        protected void Send<TMessage>(TMessage message)
        {
            var result = _bus.Send(message);

            if (result.HasException)
                //TODO: transform exception and log
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
        }
    }
}