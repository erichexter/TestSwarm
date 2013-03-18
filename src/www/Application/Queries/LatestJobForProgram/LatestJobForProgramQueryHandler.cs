using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System.Linq;

namespace nTestSwarm.Application.Queries.LatestJobForProgram
{
    public class LatestJobForProgramQueryHandler : IHandler<LatestJobForProgramQuery, long>
    {
        private readonly IDataBase _db;

        public LatestJobForProgramQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public long Handle(LatestJobForProgramQuery request)
        {
            return (from j in _db.All<Job>()
                    where j.Program.Id == request.ProgramId
                    orderby j.Created descending
                    select j.Id)
                    .FirstOrDefault();
        }
    }
}