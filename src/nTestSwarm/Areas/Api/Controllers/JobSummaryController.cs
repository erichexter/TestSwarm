using System.Web.Mvc;
using System.Web.UI;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobSummary;

namespace nTestSwarm.Areas.Api.Controllers
{
    public class JobSummaryController : ApiController
    {
        readonly IBus _bus;

        public JobSummaryController(IBus bus)
        {
            _bus = bus;
        }

        [OutputCache(CacheProfile = "jobsummary", Location = OutputCacheLocation.Server)]
        public ApiJsonResult<JobSummaryResult> Index(long id)
        {
            Result<JobSummaryResult> request = _bus.Request(new JobSummaryQuery(id));
            return ApiJson(request);
        }
    }
}