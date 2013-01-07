using System.Collections.Generic;
using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Queries.JobSummary
{
    public class JobSummaryQueryHandler : IHandler<JobSummaryQuery, JobSummaryResult>
    {
        readonly IDataBase _db;

        public JobSummaryQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public JobSummaryResult Handle(JobSummaryQuery request)
        {
            var result = new JobSummaryResult();

            var job = _db.Find<Job>(request.JobId);

            result.Started = job.Started;
            result.Finished = job.Finished;

            var browserRunResults = _db.All<RunUserAgent>()
                .Where(x => x.Run.JobId == job.Id).Select(x => x.Result).ToArray();

            result.TotalRuns = browserRunResults.Count();
            result.NotStartedRuns = Count(browserRunResults, ClientRunStatus.NotStarted);
            result.TimeoutRuns = Count(browserRunResults, ClientRunStatus.Timeout);
            result.ErrorRuns = Count(browserRunResults, ClientRunStatus.Error);
            result.InProgressRuns = Count(browserRunResults, ClientRunStatus.InProgress);
            result.FailRuns = Count(browserRunResults, ClientRunStatus.Fail);
            result.PassRuns = Count(browserRunResults, ClientRunStatus.Pass);

            result.TotalTests = browserRunResults.Sum(x => x.TotalTests.GetValueOrDefault());
            result.TotalFailedTests = browserRunResults.Sum(x => x.FailedTests.GetValueOrDefault());
            
            return result;
        }

        static int Count(IEnumerable<RunUserAgentResult> runUserAgents, ClientRunStatus status)
        {
            return runUserAgents.Count(x => x.Status == status);
        }
    }
}