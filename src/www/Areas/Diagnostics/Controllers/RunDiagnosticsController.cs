using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using nTestSwarm.Application;
using nTestSwarm.Application.Data;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Areas.Diagnostics.Controllers
{
    public class RunDiagnosticsController : Controller
    {
        readonly IBus _bus;

        public RunDiagnosticsController(IBus bus)
        {
            _bus = bus;
        }

        public ViewResult Index()
        {
            var result = _bus.Request(new RunDiagnosticsQuery());

            return View(result);
        }

        public ViewResult Nullo()
        {
            return View();
        }
    }

    public class RunDiagnosticsQuery : IRequest<IEnumerable<RunDiagnosticsResult>>
    {
        
    }

    public class RunDiagnosticsResult
    {
        public RunStatusType Status { get; set; }
        public string RunName { get; set; }
        public string JobName { get; set; }
        public long JobId { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string OperatingSystem { get; set; }
        public DateTime Updated { get; set; }

        public int? UserAgentVersion { get; set; }
    }

    public class RunDiagnosticsQueryHandler : IHandler<RunDiagnosticsQuery, IEnumerable<RunDiagnosticsResult>>
    {
        readonly nTestSwarmContext _db;

        public RunDiagnosticsQueryHandler(nTestSwarmContext db)
        {
            _db = db;
        }

        public IEnumerable<RunDiagnosticsResult> Handle(RunDiagnosticsQuery request)
        {
            var queryResult = from r in _db.RunUserAgents
                              join c in _db.Clients on r.ActiveClientId equals c.Id into clients
                              let client = clients.FirstOrDefault()
                              let ip = client == null ? string.Empty : client.IpAddress
                              let ua = r.UserAgent.Name
                              let uv = r.UserAgent.Version
                              let os = client == null ? string.Empty : client.OperatingSystem
                              where r.Status == (int) RunStatusType.Running
                              orderby r.Updated descending 
                              select new
                                  {
                                      RunName = r.Run.Name,
                                      JobName = r.Run.Job.Name,
                                      JobId = r.Run.Job.Id,
                                      IpAddress = ip,
                                      UserAgent = ua,
                                      UserAgentVersion=uv,
                                      OperatingSystem = os, 
                                      r.Updated
                                  };

            var array = queryResult.ToArray();

            if (!array.Any())
            {
                return new RunDiagnosticsResult[0];
            }

            return array.Select(x => new RunDiagnosticsResult
                {
                    RunName = x.RunName,
                    JobName = x.JobName,
                    JobId = x.JobId,
                    IpAddress = x.IpAddress,
                    UserAgent = x.UserAgent,
                    UserAgentVersion = x.UserAgentVersion,
                    OperatingSystem = x.OperatingSystem,
                    Updated = x.Updated,
                });
        }
    }
}