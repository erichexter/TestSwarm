using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.RunDiagnostics;
using nTestSwarm.Models;
using System.Linq;

namespace nTestSwarm.Api
{
    public class NeededClientsController : BusApiController
    {
        private readonly IUrlHelperWrapper _urlHelper;

        public NeededClientsController(IBus bus, IUrlHelperWrapper urlHelper)
            : base(bus)
        {
            _urlHelper = urlHelper;
        }
        
        public NeededClientResults Get()
        {
            var result = Query(new RunDiagnosticsQuery());
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

                ClientUrl = _urlHelper.Action("index", "run", new { Area = "client" })
            };

            return response;
        }
    }
}