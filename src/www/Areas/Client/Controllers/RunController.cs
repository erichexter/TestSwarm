using nTestSwarm.Application;
using nTestSwarm.Application.Commands.ClientCreation;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.Web.Mvc;

namespace nTestSwarm.Areas.Client.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class RunController : Controller
    {
        private readonly IBus _bus;

        public RunController(IBus bus)
        {
            _bus = bus;
        }

        // GET: /Client/Run/
        /// <summary>
        /// Called by a test execution host.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = _bus.Request(new CreateClient
            {
                Browser = Request.Browser.Browser,
                Version = Request.Browser.MajorVersion,
                IpAddress = Request.GetIpAddress(),
                OperatingSystem = Request.Browser.Platform
            });

            return View(model.Data);
        }
    }
}
