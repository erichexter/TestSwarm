using nTestSwarm.Application.Data;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.Collections.Generic;
using System.Linq;

namespace nTestSwarm.Application.Queries.RunDiagnostics
{
    public class RunDiagnosticsQueryHandler : IHandler<RunDiagnosticsQuery, IEnumerable<RunDiagnosticsResult>>
    {
        readonly nTestSwarmContext _db;

        public RunDiagnosticsQueryHandler(nTestSwarmContext db)
        {
            _db = db;
        }

        public IEnumerable<RunDiagnosticsResult> Handle(RunDiagnosticsQuery request)
        {
            var query = from r in _db.RunUserAgents
                        join c in _db.Clients on r.ActiveClientId equals c.Id into clients
                        let client = clients.FirstOrDefault()
                        let ip = client == null ? string.Empty : client.IpAddress
                        let os = client == null ? string.Empty : client.OperatingSystem
                        where r.RunStatus == RunStatusType.Running
                        orderby r.Updated descending
                        select new RunDiagnosticsResult
                        {
                            RunName = r.Run.Name,
                            JobName = r.Run.Job.Name,
                            JobId = r.Run.Job.Id,
                            IpAddress = ip,
                            UserAgent = r.UserAgent.Name,
                            UserAgentVersion = r.UserAgent.Version,
                            OperatingSystem = os,
                            Updated = r.Updated
                        };

            return query.ToArray();
        }
    }
}