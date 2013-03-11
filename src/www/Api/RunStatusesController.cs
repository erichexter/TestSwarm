using nTestSwarm.Application.Commands.CompletedRun;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.RunStatus;
using System.Web.Http;

namespace nTestSwarm.Api
{
    public class RunStatusesController : BusApiController
    {
        public RunStatusesController(IBus bus) : base(bus)
        {
        }

        public RunStatusResult Get([FromBody]RunStatusQuery query)
        {
            return Query(query);
        }

        public void Post([FromBody]CompleteRun input)
        {
            Send(input);
        }
    }
}