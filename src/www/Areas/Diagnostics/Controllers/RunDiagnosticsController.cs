using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.RunDiagnostics;
using nTestSwarm.Controllers;
using System.Web.Mvc;

namespace nTestSwarm.Areas.Diagnostics.Controllers
{
    public class RunDiagnosticsController : BusController
    {
        public RunDiagnosticsController(IBus bus) : base(bus) { }

        public ActionResult Index()
        {
            return Query(new RunDiagnosticsQuery());
        }

        public ViewResult Nullo()
        {
            return View();
        }
    }
}