using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System.Linq;

namespace nTestSwarm.Application.Queries.JobDetails
{
    public class JobDetailsQueryHandler : IHandler<JobDetailsQuery, JobDetailsViewModel>
    {
        private readonly IDataBase _db;

        public JobDetailsQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public JobDetailsViewModel Handle(JobDetailsQuery request)
        {
            return _db.All<Job>()
                    .Where(j => j.Id == request.Id)
                    .Select(j => new JobDetailsViewModel
                    {
                        JobId = j.Id,
                        ProgramId = j.Program.Id
                    }).First();
        }
    }
}