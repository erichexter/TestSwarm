using System.Linq;
using System.Web.Mvc;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Repositories;

namespace nTestSwarm.Areas.Admin.Controllers
{   
    public class RunsController : Controller
    {
		private readonly IRepository<Job> jobRepository;
		private readonly IRepository<Run> runRepository;

		// If you are using Dependency Injection, you can delete the following constructor

        public RunsController(IRepository<Job> jobRepository, IRepository<Run> runRepository)
        {
			this.jobRepository = jobRepository;
			this.runRepository = runRepository;
        }

        //
        // GET: /Runs/

        public ViewResult Index()
        {
            return View(runRepository.AllIncluding(run => run.Job));
        }

        //
        // GET: /Runs/Details/5

        public ViewResult Details(long id)
        {
            var model = runRepository.AllIncluding(x => x.RunUserAgents).Where(x => x.Id == id).Single();

            return View(model);
        }

        //
        // GET: /Runs/Create

        public ActionResult Create()
        {
			ViewBag.PossibleJobs = jobRepository.All;
            return View();
        } 

        //
        // POST: /Runs/Create

        [HttpPost]
        public ActionResult Create(Run run)
        {
            if (ModelState.IsValid) {
                runRepository.InsertOrUpdate(run);
                runRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleJobs = jobRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Runs/Edit/5
 
        public ActionResult Edit(long id)
        {
			ViewBag.PossibleJobs = jobRepository.All;
             return View(runRepository.Find(id));
        }

        //
        // POST: /Runs/Edit/5

        [HttpPost]
        public ActionResult Edit(Run run)
        {
            if (ModelState.IsValid) {
                runRepository.InsertOrUpdate(run);
                runRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleJobs = jobRepository.All;
				return View();
			}
        }

        //
        // GET: /Runs/Delete/5
 
        public ActionResult Delete(long id)
        {
            return View(runRepository.Find(id));
        }

        //
        // POST: /Runs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            runRepository.Delete(id);
            runRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

