using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.NextRun;
using System.Web.Http;

namespace nTestSwarm.Api
{
    public class ClientsController : BusApiController
    {
        public ClientsController(IBus bus) : base(bus) { }

        [HttpGet]
        public NextRunResult NextRun(long id)
        {
            return Query(new NextRunQuery(id));
        }
    }
}
