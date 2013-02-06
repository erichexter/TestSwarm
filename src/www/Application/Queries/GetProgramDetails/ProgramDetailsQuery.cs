using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Queries.GetProgramDetails
{
    public class ProgramDetailsQuery : IRequest<ProgramDetailsViewModel>
    {
        public long ProgramId { get; set; }
    }
}