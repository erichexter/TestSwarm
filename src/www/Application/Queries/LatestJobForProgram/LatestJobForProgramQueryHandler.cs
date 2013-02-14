using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using nTestSwarm.Areas.Api.Models;
using System.Linq;

namespace nTestSwarm.Application.Queries.LatestJobForProgram
{
    public class LatestJobForProgramQueryHandler : IHandler<LatestJobForProgramQuery, ProgramLatestJobViewModel>
    {
        private readonly IDataBase _db;

        public LatestJobForProgramQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public ProgramLatestJobViewModel Handle(LatestJobForProgramQuery request)
        {
            return (from j in _db.All<Job>()
                    where j.Program.Id == request.ProgramId
                    orderby j.Created descending
                    select new ProgramLatestJobViewModel
                    {
                        ProgramId = j.Program.Id,
                        JobId = j.Id
                    })
                    .FirstOrDefault();
        }
    }
}