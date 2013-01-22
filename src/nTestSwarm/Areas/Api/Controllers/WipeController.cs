using System.Web.Mvc;
using nTestSwarm.Application.Commands.DataCleanup;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Areas.Api.Controllers
{
    public class WipeController : ApiController
    {
        readonly IBus _bus;

        public WipeController(IBus bus)
        {
            _bus = bus;
        }

        public ViewResult Index()
        {
            return View();
        }
        
        public ApiStringResult DoWipe()
        {
            var result = _bus.Send(new Wipe());

            return ApiString(result, "done");
        }
    }
}