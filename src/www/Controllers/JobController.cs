using nTestSwarm.Application.Commands.JobDeletion;
using nTestSwarm.Application.Commands.JobResetting;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobStatus;
using System;
using System.Web.Mvc;
using System.Web.UI;

namespace nTestSwarm.Controllers
{
    public class JobController : Controller
    {
        readonly IBus _bus;

        public JobController(IBus bus)
        {
            _bus = bus;
        }

        [OutputCache(CacheProfile = "jobstatus", Location = OutputCacheLocation.Server)]
        public ViewResult Index(long id)
        {
            var viewAndData = GetViewAndData(id);
            return View(viewAndData.Item1, viewAndData.Item2);
        }

        [OutputCache(CacheProfile = "jobstatus", Location = OutputCacheLocation.Server)]
        public PartialViewResult StatusTable(long id)
        {
            var viewAndData = GetViewAndData(id);
            return PartialView(viewAndData.Item1, viewAndData.Item2);
        }

        public ViewResult Latest()
        {
            var result = _bus.Request(new LatestJobStatusQuery());

            if (result.HasException || result.Data.IsEmpty)
            {
                return View("NoJob", result.Exception);
            }

            return View("Index", result.Data);
        }

        Tuple<string, object> GetViewAndData(long id)
        {
            var result = _bus.Request(new JobStatusQuery(id));

            if (result.HasException)
            {
                return new Tuple<string, object>("NoJob", result.Exception);
            }

            return new Tuple<string, object>(string.Empty, result.Data);
        }
            
        [HttpPost]
        public ActionResult WipeJob(string type, long job_id)
        {
            if (string.Equals(type, "reset", StringComparison.OrdinalIgnoreCase))
            {
                _bus.Send(new ResetJob(job_id));
                return RedirectToAction("Index", new {id = job_id, area = string.Empty});
            }
            if (string.Equals(type, "delete", StringComparison.OrdinalIgnoreCase))
            {
                _bus.Send(new DeleteJob(job_id));
                return RedirectToAction("Index", "Jobs", new {area = "Admin"});
            }
            throw new Exception("Invalid type for WipeJob");
        }

    }
}