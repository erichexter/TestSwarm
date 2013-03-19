using System.Diagnostics;
using System.Linq;
using nTestSwarm.Application.Data;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Queries.JobStatus
{
    public class JobStatusQueryHandler : IHandler<JobStatusQuery, JobStatusResult>
    {
        readonly nTestSwarmContext _context;

        public JobStatusQueryHandler(nTestSwarmContext context)
        {
            _context = context;
        }

        public JobStatusResult Handle(JobStatusQuery request)
        {
            var results = from rua in _context.RunUserAgents
                          let run = rua.Run
                          let jon = run.Job
                          let ua = rua.UserAgent
                          where jon.Id == request.JobId
                          select new ResultDto
                              {
                                  RunId = run.Id,
                                  RunName = run.Name,
                                  RunUrl = run.Url,
                                  JobId = jon.Id,
                                  JobName = jon.Name,
                                  UserAgentId = ua.Id,
                                  UserAgentBrowser = ua.Browser,
                                  UserAgentName = ua.Name,
                                  UserAgentVersion = ua.Version,
                                  ClientId = rua.Result.ClientId,
                                  CellContents = rua.Result.CellContents,
                                  StatusValue = rua.Result.StatusValue
                              };

            Debug.WriteLine(results.ToString());

            return new JobStatusResult(results.ToArray());
        }
    }
}