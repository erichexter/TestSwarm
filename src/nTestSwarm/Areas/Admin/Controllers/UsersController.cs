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
    public class UsersController : Controller
    {
		private readonly IRepository<User> userRepository;
        
        public UsersController(IRepository<User> userRepository)
        {
			this.userRepository = userRepository;
        }

        //
        // GET: /Users/

        public ViewResult Index()
        {
            return View(userRepository.All);
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(long id)
        {
            return View(userRepository.Find(id));
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(long id)
        {
             return View(userRepository.Find(id));
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Users/Delete/5
 
        public ActionResult Delete(long id)
        {
            return View(userRepository.Find(id));
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            userRepository.Delete(id);
            userRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

