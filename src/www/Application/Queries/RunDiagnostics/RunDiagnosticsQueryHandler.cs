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
            // left join RunUserAgents to Clients to obtain the running runs
            var query = from r in _db.RunUserAgents
                        from c in _db.Clients.Where(c => c.Id == r.ActiveClientId).DefaultIfEmpty()
                        where r.RunStatus == RunStatusType.Running
                        orderby r.Updated descending
                        select new RunDiagnosticsResult
                        {
                            RunName = r.Run.Name,
                            JobName = r.Run.Job.Name,
                            JobId = r.Run.Job.Id,
                            IpAddress = c == null ? string.Empty : c.IpAddress,
                            UserAgent = r.UserAgent.Name,
                            UserAgentVersion = r.UserAgent.Version,
                            OperatingSystem = c == null ? string.Empty : c.OperatingSystem,
                            Updated = r.Updated
                        };

            return query.ToArray();
        }
    }
}