using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobList;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Queries.LatestJobList
{
    public class LatestJobListQuery : IRequest<IEnumerable<JobListResult>>
    {
        public LatestJobListQuery(int numberOfJobs)
        {
            NumberOfJobs = numberOfJobs;
        }

        public int NumberOfJobs { get; set; }
    }

    public class LatestJobListQueryHandler : IHandler<LatestJobListQuery, IEnumerable<JobListResult>>
    {
        readonly IDataBase _db;

        public LatestJobListQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public IEnumerable<JobListResult> Handle(LatestJobListQuery request)
        {
            return (from job in _db.All<Job>().AsNoTracking()
                   orderby job.Created descending 
                   select new JobListResult
                   {
                       JobId = job.Id,
                       Created = job.Created,
                       Completed = job.Finished,
                       JobName = job.Name,
                       Type = (JobStatusType) job.Status
                   }).ToArray().Yield(x => x.Status = x.Type.ToString()); 
        }
    }
}