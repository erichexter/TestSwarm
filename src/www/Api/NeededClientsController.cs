using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.RunDiagnostics;
using nTestSwarm.Models;
using System.Linq;

namespace nTestSwarm.Api
{
    public class NeededClientsController : BusApiController
    {
        public NeededClientsController(IBus bus) : base(bus) { }
        
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

                //TODO: we can't get access to the MVC routes from Web API. need to figure this out.
                //ClientUrl = 
            };

            return response;
        }
    }
}