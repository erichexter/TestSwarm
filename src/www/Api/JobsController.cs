using nTestSwarm.Application.Commands.JobDeletion;
using nTestSwarm.Application.Commands.JobResetting;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.Web.Http;

namespace nTestSwarm.Api
{
    public class JobsController : BusApiController
    {
        public JobsController(IBus bus) : base(bus) { }

        public void Delete(long id)
        {
            Send(new DeleteJob(id));
        }

        [HttpGet]
        public void Remove(long id)
        {
            Delete(id);
        }

        [HttpPut]
        [HttpGet]
        public void Reset(long id)
        {
            Send(new ResetJob(id));
        }
    }
}
