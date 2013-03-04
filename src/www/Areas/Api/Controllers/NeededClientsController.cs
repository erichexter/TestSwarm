using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Areas.Utils.Controllers;

namespace nTestSwarm.Areas.Api.Controllers
{
    public class NeededClientsController : ApiController
    {
        private readonly IBus _bus;

        public NeededClientsController(IBus bus)
        {
            _bus = bus;
        }

        // GET api/<controller>
        public ActionResult Index()
        {
            var result = _bus.Request(new RunDiagnosticsQuery());
            NeededClientResults model = new NeededClientResults();
            model.UserAgents = result.Data.Where(d => string.IsNullOrWhiteSpace(d.IpAddress)).Select(r=>new NeededClient()
                {
                    Browser = r.UserAgent,
                    Version = r.UserAgentVersion,
                }).Distinct().ToList();
            model.Jobs = result.Data.Select(e => e.JobId).Distinct().ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }

    public class NeededClientResults
    {
        public List<NeededClient> UserAgents { get; set; }

        public List<long> Jobs { get; set; }
    }

    public class NeededClient
    {
        public string Browser { get; set; }

        public int? Version { get; set; }

    }
}