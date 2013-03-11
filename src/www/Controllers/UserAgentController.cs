using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.UserAgentInfo;
using System.Web.Mvc;

namespace nTestSwarm.Controllers
{
    public class UserAgentController : BusController
    {
        public UserAgentController(IBus bus) : base(bus) { }

        public ActionResult UserAgentInfo(UserAgentInfoQuery input)
        {
            return Query(input, PartialView);
        }
    }
}