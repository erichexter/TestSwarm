using nTestSwarm.Application.Commands.ClientCreation;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.NextRun;
using nTestSwarm.Models;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace nTestSwarm.Api
{
    public class ClientsController : BusApiController
    {
        public ClientsController(IBus bus) : base(bus) { }

        public void Post([ModelBinder(typeof(ClientModelBinder))] CreateClient input)
        {
            Send(input);
        }

        [HttpGet]
        public NextRunResult NextRun(long id)
        {
            return Query(new NextRunQuery(id));
        }
    }
}
