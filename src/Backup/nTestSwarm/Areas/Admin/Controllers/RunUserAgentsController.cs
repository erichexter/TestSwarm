using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Repositories;
using nTestSwarm.Models;
using nTestSwarm.Areas.Admin.Models;

namespace nTestSwarm.Areas.Admin.Controllers
{   
    public class RunUserAgentsController : Controller
    {
		private readonly IRepository<UserAgent> useragentRepository;
		private readonly IRepository<RunUserAgent> runuseragentRepository;


        public RunUserAgentsController(IRepository<UserAgent> useragentRepository, IRepository<RunUserAgent> runuseragentRepository)
        {
			this.useragentRepository = useragentRepository;
			this.runuseragentRepository = runuseragentRepository;
        }

        //
        // GET: /RunUserAgents/

        public ViewResult Index()
        {
            return View(runuseragentRepository.AllIncluding(runuseragent => runuseragent.UserAgent));
        }

        //
        // GET: /RunUserAgents/Details/5

        public ViewResult Details(long id)
        {
            return View(runuseragentRepository.Find(id));
        }

        //
        // GET: /RunUserAgents/Create

        public ActionResult Create()
        {
			ViewBag.PossibleUserAgents = useragentRepository.All;
            return View();
        } 

        //
        // POST: /RunUserAgents/Create

        [HttpPost]
        public ActionResult Create(RunUserAgent runuseragent)
        {
            if (ModelState.IsValid) {
                runuseragentRepository.InsertOrUpdate(runuseragent);
                runuseragentRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUserAgents = useragentRepository.All;
				return View();
			}
        }
        
        //
        // GET: /RunUserAgents/Edit/5
 
        public ActionResult Edit(long id)
        {
			ViewBag.PossibleUserAgents = useragentRepository.All;
             return View(runuseragentRepository.Find(id));
        }

        //
        // POST: /RunUserAgents/Edit/5

        [HttpPost]
        public ActionResult Edit(RunUserAgent runuseragent)
        {
            if (ModelState.IsValid) {
                runuseragentRepository.InsertOrUpdate(runuseragent);
                runuseragentRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUserAgents = useragentRepository.All;
				return View();
			}
        }

        //
        // GET: /RunUserAgents/Delete/5
 
        public ActionResult Delete(long id)
        {
            return View(runuseragentRepository.Find(id));
        }

        //
        // POST: /RunUserAgents/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            runuseragentRepository.Delete(id);
            runuseragentRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

