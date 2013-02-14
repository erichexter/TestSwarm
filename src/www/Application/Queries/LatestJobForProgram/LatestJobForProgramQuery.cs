using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Areas.Api.Models;

namespace nTestSwarm.Application.Queries.LatestJobForProgram
{
    public class LatestJobForProgramQuery : IRequest<ProgramLatestJobViewModel>
    {
        public long ProgramId { get; set; }
    }
}