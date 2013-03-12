using System.Web.Mvc;

namespace nTestSwarm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Program");
        }
    }
}