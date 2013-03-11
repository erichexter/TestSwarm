using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Areas.Api.Controllers;
using nTestSwarm.Areas.Utils.Controllers;
using System.Linq;

namespace nTestSwarm.Api
{
    public class NeededClientsController : BusApiController
    {
        public NeededClientsController(IBus bus) : base(bus)
        {
        }
        
        public NeededClientResults Get()
        {
            var result = Request(new RunDiagnosticsQuery());
            var response = new NeededClientResults
            {
                UserAgents = result
                                .Where(d => string.IsNullOrWhiteSpace(d.IpAddress))
                                .Select(r => new NeededClient
                                {
                                    Browser = r.UserAgent,
                                    Version = r.UserAgentVersion,
                                }).Distinct().ToList(),

                Jobs = result.Select(e => e.JobId).Distinct().ToList(),

                ClientUrl = Url.Link(WebApiConfig.DefaultRoute, new { controller = "clients" })
            };

            return response;
        }
    }
}