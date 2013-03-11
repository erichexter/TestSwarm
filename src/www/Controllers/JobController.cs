using nTestSwarm.Application.Commands.JobCreation;
using nTestSwarm.Application.Commands.JobCreation.Described;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobStatus;
using nTestSwarm.Models;
using System.Web.Mvc;
using System.Web.UI;

namespace nTestSwarm.Controllers
{
    public class JobController : BusController
    {
        public JobController(IBus bus) : base(bus) { }

        [OutputCache(CacheProfile = "jobstatus", Location = OutputCacheLocation.Server)]
        public ActionResult Details(long id)
        {
            return Query(new JobStatusQuery(id), null, ex => View("NoJob", ex));
        }

        public ActionResult Latest()
        {
            return Query(new LatestJobStatusQuery(), 
                         r => RedirectToAction("Details", new { id = r.JobId }), 
                         ex => View("NoJob"));
        }

        public ViewResult Create()
        {
            return View(new CreateJobInput());
        }

        [HttpPost]
        public ActionResult Create(CreateJob input)
        {
            return Query(input);
        }

        public ViewResult DescribeNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DescribeNew(CreateJobFromDescription input)
        {
            return Query(input);
        }
    }
}