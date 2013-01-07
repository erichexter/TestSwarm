using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Queries.JobList
{
    public class JobListQuery : IRequest<IEnumerable<JobListResult>>
    {
    }

    public class JobListResult
    {
        public long JobId { get; set; }
        public string JobName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }
        public JobStatusType Type { get; set; }
        public string Status { get; set; }
    }

    public class JobListQueryHandler : IHandler<JobListQuery, IEnumerable<JobListResult>>
    {
        readonly IDataBase _db;

        public JobListQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public IEnumerable<JobListResult> Handle(JobListQuery request)
        {
            return (from job in _db.All<Job>().AsNoTracking()
                   orderby job.Created
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