using System.Web.Mvc;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobList;
using nTestSwarm.Application.Queries.LatestJobList;

namespace nTestSwarm.Areas.Api.Controllers
{
    public class JobListController : ApiController
    {
        readonly IBus _bus;

        public JobListController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public ActionResult All()
        {
            var result = _bus.Request(new JobListQuery());

            return ApiJson(result);
        }

        [HttpGet]
        public ActionResult Latest(int numberOfJobs = 10)
        {
            var result = _bus.Request(new LatestJobListQuery(numberOfJobs));

            return ApiJson(result);
        }
    }
}