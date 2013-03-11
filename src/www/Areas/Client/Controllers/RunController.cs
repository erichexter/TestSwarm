using nTestSwarm.Application;
using nTestSwarm.Application.Commands.ClientCreation;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Controllers;
using System.Web.Mvc;

namespace nTestSwarm.Areas.Client.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class RunController : BusController
    {
        public RunController(IBus bus) : base(bus) { }

        // GET: /Client/Run/
        /// <summary>
        /// Called by a test execution host.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var command = new CreateClient
            {
                Browser = Request.Browser.Browser,
                Version = Request.Browser.MajorVersion,
                IpAddress = Request.GetIpAddress(),
                OperatingSystem = Request.Browser.Platform
            };

            return Query(command);
        }
    }
}
