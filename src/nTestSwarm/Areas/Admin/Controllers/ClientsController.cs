using System.Web.Mvc;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Repositories;

namespace nTestSwarm.Areas.Admin.Controllers
{   
    public class ClientsController : Controller
    {
		private readonly IRepository<User> userRepository;
		private readonly IRepository<UserAgent> useragentRepository;
		private readonly IRepository<Client> clientRepository;
        
        public ClientsController(IRepository<User> userRepository, IRepository<UserAgent> useragentRepository, IRepository<Client> clientRepository)
        {
			this.userRepository = userRepository;
			this.useragentRepository = useragentRepository;
			this.clientRepository = clientRepository;
        }

        //
        // GET: /Clients/

        public ViewResult Index()
        {
            return View(clientRepository.AllIncluding(client => client.UserAgent));
        }

        //
        // GET: /Clients/Details/5

        public ViewResult Details(long id)
        {
            return View(clientRepository.Find(id));
        }

        //
        // GET: /Clients/Create

        public ActionResult Create()
        {
			ViewBag.PossibleUsers = userRepository.All;
			ViewBag.PossibleUserAgents = useragentRepository.All;
            return View();
        } 

        //
        // POST: /Clients/Create

        [HttpPost]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid) {
                clientRepository.InsertOrUpdate(client);
                clientRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUsers = userRepository.All;
				ViewBag.PossibleUserAgents = useragentRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Clients/Edit/5
 
        public ActionResult Edit(long id)
        {
			ViewBag.PossibleUsers = userRepository.All;
			ViewBag.PossibleUserAgents = useragentRepository.All;
             return View(clientRepository.Find(id));
        }

        //
        // POST: /Clients/Edit/5

        [HttpPost]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid) {
                clientRepository.InsertOrUpdate(client);
                clientRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUsers = userRepository.All;
				ViewBag.PossibleUserAgents = useragentRepository.All;
				return View();
			}
        }

        //
        // GET: /Clients/Delete/5
 
        public ActionResult Delete(long id)
        {
            return View(clientRepository.Find(id));
        }

        //
        // POST: /Clients/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            clientRepository.Delete(id);
            clientRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

