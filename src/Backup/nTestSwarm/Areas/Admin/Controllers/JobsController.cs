using System.Web.Mvc;
using nTestSwarm.Application;
using nTestSwarm.Application.Commands.JobCreation.Copy;
using nTestSwarm.Application.Commands.JobDeletion;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Repositories;
using nTestSwarm.Areas.Admin.Models;

namespace nTestSwarm.Areas.Admin.Controllers
{
    public class JobsController : Controller
    {
        readonly IBus _bus;
        readonly IRepository<Job> jobRepository;

        public JobsController(IRepository<Job> jobRepository, IBus bus)
        {
            this.jobRepository = jobRepository;
            _bus = bus;
        }

        //
        // GET: /Jobs/

        public ViewResult Index()
        {
            return View(jobRepository.All);
        }

        //
        // GET: /Jobs/Details/5

        public ViewResult Details(long id)
        {
            var model = jobRepository.Find(id);
            return View(model);
        }

        //
        // GET: /Jobs/Create

        public ActionResult Create()
        {
            return View(new Job());
        }

        //
        // POST: /Jobs/Create

        [HttpPost]
        public ActionResult Create(Job job)
        {
            if (ModelState.IsValid)
            {
                jobRepository.InsertOrUpdate(job);
                jobRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(job);
            }
        }

        //
        // GET: /Jobs/Edit/5

        public ActionResult Edit(long id)
        {
            return View(jobRepository.Find(id));
        }

        //
        // POST: /Jobs/Edit/5

        [HttpPost]
        public ActionResult Edit(Job job)
        {
            if (ModelState.IsValid)
            {
                jobRepository.InsertOrUpdate(job);
                jobRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Jobs/Delete/5

        public ActionResult Delete(long id)
        {
            return View(jobRepository.Find(id));
        }

        //
        // POST: /Jobs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            _bus.Send(new DeleteJob(id));

            return RedirectToAction("Index");
        }

        public ActionResult Copy(long id)
        {
            return View(BuildJobCopyModel(id));
        }

        [HttpPost, ActionName("Copy")]
        public ActionResult CopyConfirmed([Bind(Prefix = "Defaults")] JobCopyInputModel input)
        {
            if (ModelState.IsValid)
            {
                var request = _bus.Request(new CopyJob(input));

                return RedirectToAction("Index");
            }
            var model = BuildJobCopyModel(input.JobId);
            return View("Copy", model);
        }

        JobCopyModel BuildJobCopyModel(long id)
        {
            var job = jobRepository.Find(id);

            var model = new JobCopyModel
                {
                    JobToCopy = job,
                    RunsForJob = job.Runs,
                    Defaults =
                        new JobCopyInputModel
                            {JobId = job.Id, JobNameFormat = "Job {0}", Correlation = (job.Id + 1).ToString()}
                };
            return model;
        }
    }
}