using nTestSwarm.Application.Commands.ClientCreation;
using nTestSwarm.Models;
using System.Web.Mvc;

namespace nTestSwarm.Areas.Admin.Controllers
{
    public class ClientsController : Controller
    {

        [HttpGet]
        public ActionResult New([ModelBinder(typeof(ClientModelBinder))] CreateClient input)
        {
            return View();
        }

    }
}
