using nTestSwarm.Application.Events.JobCompletion;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobList;
using System.Web.Mvc;

namespace nTestSwarm.Controllers
{
    public class JobDiffController : BusController
    {
        public JobDiffController(IBus bus) : base(bus) { }

        public ActionResult Index()
        {
            return Query(new JobListQuery());
        }

        public ActionResult ShowDiff(JobDiffQuery input)
        {
            return Query(input);
        }
    }
}