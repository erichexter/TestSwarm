using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using nTestSwarm.Models;
using System.Linq;

namespace nTestSwarm.Application.Queries.LatestJobForProgram
{
    public class LatestJobForProgramQueryHandler : IHandler<LatestJobForProgramQuery, JobDetailsViewModel>
    {
        private readonly IDataBase _db;

        public LatestJobForProgramQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public JobDetailsViewModel Handle(LatestJobForProgramQuery request)
        {
            return (from j in _db.All<Job>()
                    where j.Program.Id == request.ProgramId
                    orderby j.Created descending
                    select new JobDetailsViewModel
                    {
                        ProgramId = j.Program.Id,
                        JobId = j.Id
                    })
                    .FirstOrDefault();
        }
    }
}