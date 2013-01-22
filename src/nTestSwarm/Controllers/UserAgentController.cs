using System.Web.Mvc;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.UserAgentInfo;

namespace nTestSwarm.Controllers
{
    public class UserAgentController : Controller
    {
        readonly IBus _bus;

        public UserAgentController(IBus bus)
        {
            _bus = bus;
        }

        public PartialViewResult UserAgentInfo(UserAgentInfoQuery input)
        {
            var response = _bus.Request(input);

            return PartialView(response.Data);
        }
    }
}