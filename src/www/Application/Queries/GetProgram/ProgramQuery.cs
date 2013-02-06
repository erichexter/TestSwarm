using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Queries.GetProgram
{
    public class ProgramQuery : IRequest<ProgramViewModel>
    {
        public long ProgramId { get; set; }
    }
}