using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nTestSwarm.Areas.Api.Controllers
{
    public class RunController : Controller
    {
        public RunController()
        {
        }
        public ActionResult RemoveTimedOutClients()
        {
            //remove run client
            //decrement run_useragent
            //finish run
            //finish job?
            return View();
        }

    }
}
