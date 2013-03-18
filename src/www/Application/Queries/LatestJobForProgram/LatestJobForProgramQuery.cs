using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Queries.LatestJobForProgram
{
    public class LatestJobForProgramQuery : IRequest<long?>
    {
        public LatestJobForProgramQuery(int programId)
        {
            ProgramId = programId;
        }

        public long ProgramId { get; private set; }
    }
}