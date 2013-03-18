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
            return View(id);
        }

        /// <summary>
        /// Determines the last job that was run (or is running) and redirects 
        /// to the Details action for the given job.
        /// </summary>
        public ActionResult Latest()
        {
            return Query(new LatestJobStatusQuery(), 
                         r => RedirectToAction("Details", new { id = r.JobId }), 
                         ex => View("NoJob"));
        }

        // placeholder for named routes
        public ActionResult Nullo()
        {
            return View();
        }
    }
}