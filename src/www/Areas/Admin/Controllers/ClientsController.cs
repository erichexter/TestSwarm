using nTestSwarm.Models;
using System.Web.Mvc;

namespace nTestSwarm.Areas.Admin.Controllers
{
    public class ClientsController : Controller
    {

        [HttpGet]
        public ActionResult New([ModelBinder(typeof(ClientModelBinder))] ClientInputModel input)
        {
            return View();
        }

    }
}
