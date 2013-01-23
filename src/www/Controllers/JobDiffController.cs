using System.Web.Mvc;
using nTestSwarm.Application.Events.JobCompletion;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobList;

namespace nTestSwarm.Controllers
{
    public class JobDiffController : Controller
    {
        readonly IBus _bus;

        public JobDiffController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public ViewResult Index()
        {
            var request = _bus.Request(new JobListQuery());

            return View();
        }

        [HttpGet]
        public ActionResult ShowDiff(JobDiffQuery input)
        {
            var result = _bus.Request(input);
            
            return View(result);
        }
    }
}