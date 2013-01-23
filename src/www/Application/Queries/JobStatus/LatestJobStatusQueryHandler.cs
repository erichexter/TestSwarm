using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Queries.JobStatus
{
    public class LatestJobStatusQueryHandler : IHandler<LatestJobStatusQuery, JobStatusResult>
    {
        readonly IDataBase _db;
        readonly IHandler<JobStatusQuery, JobStatusResult> _handler;

        public LatestJobStatusQueryHandler(IDataBase db, IHandler<JobStatusQuery, JobStatusResult> handler)
        {
            _db = db;
            _handler = handler;
        }

        public JobStatusResult Handle(LatestJobStatusQuery request)
        {
            long id = GetLatestJobId();

            return _handler.Handle(new JobStatusQuery(id));
        }

        long GetLatestJobId()
        {
            return _db.All<Job>().OrderByDescending(x => x.Started).Select(x => x.Id).Take(1).Single();
        }
    }
}