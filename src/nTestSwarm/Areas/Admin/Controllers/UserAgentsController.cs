using System.Web.Mvc;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Repositories;

namespace nTestSwarm.Areas.Admin.Controllers
{
    public class UserAgentsController : Controller
    {
        readonly IRepository<UserAgent> _useragentRepository;

        public UserAgentsController(IRepository<UserAgent> useragentRepository)
        {
            _useragentRepository = useragentRepository;
        }

        //
        // GET: /UserAgents/

        public ViewResult Index()
        {
            return View(_useragentRepository.All);
        }

        //
        // GET: /UserAgents/Details/5

        public ViewResult Details(long id)
        {
            return View(_useragentRepository.Find(id));
        }

        //
        // GET: /UserAgents/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserAgents/Create

        [HttpPost]
        public ActionResult Create(UserAgent useragent)
        {
            if (ModelState.IsValid)
            {
                _useragentRepository.InsertOrUpdate(useragent);
                _useragentRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /UserAgents/Edit/5

        public ActionResult Edit(long id)
        {
            return View(_useragentRepository.Find(id));
        }

        //
        // POST: /UserAgents/Edit/5

        [HttpPost]
        public ActionResult Edit(UserAgent useragent)
        {
            if (ModelState.IsValid)
            {
                _useragentRepository.InsertOrUpdate(useragent);
                _useragentRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /UserAgents/Delete/5

        public ActionResult Delete(long id)
        {
            return View(_useragentRepository.Find(id));
        }

        //
        // POST: /UserAgents/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            _useragentRepository.Delete(id);
            _useragentRepository.Save();

            return RedirectToAction("Index");
        }
    }
}