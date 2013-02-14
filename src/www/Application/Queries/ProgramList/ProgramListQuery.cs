using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace nTestSwarm.Application.Queries.ProgramList
{
    public class ProgramListQuery : IRequest<IEnumerable<ProgramListResult>>
    {
        public long? ProgramId { get; set; }
    }

    public class ProgramListResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool HasJobs { get { return JobCount > 0; } }
        public int JobCount { get; set; }
        public JobStatusType LastJobStatus { get; set; }
        public string LastJobStatusText { get { return JobCount == 0 ? "" : LastJobStatus.ToString(); } }
    }

    public class ProgramListQueryHandler : IHandler<ProgramListQuery, IEnumerable<ProgramListResult>>
    {

        private readonly IDataBase _db;

        public ProgramListQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public IEnumerable<ProgramListResult> Handle(ProgramListQuery request)
        {
            return (from p in _db.All<Program>().AsNoTracking()
                    orderby p.Name
                    select new ProgramListResult
                    {
                        Id = p.Id,
                        Name = p.Name,
                        JobCount = p.Jobs.Count(),
                        LastJobStatus = (from j in p.Jobs
                                        orderby j.Id descending
                                        select j.Status).FirstOrDefault()
                    }).ToArray();
        }

    }
}