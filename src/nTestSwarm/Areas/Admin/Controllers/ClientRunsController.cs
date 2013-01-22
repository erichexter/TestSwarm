using System.Web.Mvc;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Repositories;
using nTestSwarm.Areas.Admin.Models;

namespace nTestSwarm.Areas.Admin.Controllers
{   
    public class ClientRunsController : Controller
    {
		private readonly IRepository<Client> clientRepository;
		private readonly IRepository<ClientRun> clientrunRepository;


        public ClientRunsController(IRepository<Client> clientRepository, IRepository<ClientRun> clientrunRepository)
        {
			this.clientRepository = clientRepository;
			this.clientrunRepository = clientrunRepository;
        }

        //
        // GET: /ClientRuns/

        public ViewResult Index()
        {
            return View(clientrunRepository.AllIncluding(clientrun => clientrun.Client));
        }

        //
        // GET: /ClientRuns/Details/5

        public ViewResult Details(long id)
        {
            return View(clientrunRepository.Find(id));
        }

        //
        // GET: /ClientRuns/Create

        public ActionResult Create()
        {
			ViewBag.PossibleClients = clientRepository.All;
            return View();
        } 

        //
        // POST: /ClientRuns/Create

        [HttpPost]
        public ActionResult Create(ClientRun clientrun)
        {
            if (ModelState.IsValid) {
                clientrunRepository.InsertOrUpdate(clientrun);
                clientrunRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleClients = clientRepository.All;
				return View();
			}
        }
        
        //
        // GET: /ClientRuns/Edit/5
 
        public ActionResult Edit(long id)
        {
			ViewBag.PossibleClients = clientRepository.All;
             return View(clientrunRepository.Find(id));
        }

        //
        // POST: /ClientRuns/Edit/5

        [HttpPost]
        public ActionResult Edit(ClientRun clientrun)
        {
            if (ModelState.IsValid) {
                clientrunRepository.InsertOrUpdate(clientrun);
                clientrunRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleClients = clientRepository.All;
				return View();
			}
        }

        //
        // GET: /ClientRuns/Delete/5
 
        public ActionResult Delete(long id)
        {
            return View(clientrunRepository.Find(id));
        }

        //
        // POST: /ClientRuns/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            clientrunRepository.Delete(id);
            clientrunRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

