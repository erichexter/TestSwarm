using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.RunStatus;
using System.Text;
using System.Web.Mvc;

namespace nTestSwarm.Controllers
{
    public class RunController : BusController
    {
        public RunController(IBus bus) : base(bus) { }

        public ActionResult Status(RunStatusQuery input)
        {
            return Query(input, r => Content(r.Results, "text/html", Encoding.UTF8));
        }
    }
}
