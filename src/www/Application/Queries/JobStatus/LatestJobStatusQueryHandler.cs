using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System.Data.Entity;
using System.Linq;

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
            var id = GetLatestJobId();

            if (id.HasValue)
                return _handler.Handle(new JobStatusQuery(id.Value));
            else
                return new JobStatusResult();
        }

        long? GetLatestJobId()
        {
            var result = _db.All<Job>().AsNoTracking()
                            .OrderByDescending(j => j.Id)
                            .Select(j => new { Id = j.Id })
                            .FirstOrDefault();

            return result == null ? (long?)null : result.Id;
        }
    }
}