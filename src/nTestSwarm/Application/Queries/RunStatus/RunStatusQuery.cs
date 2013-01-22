using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Queries.RunStatus
{
    public class RunStatusQuery : IRequest<RunStatusResult>
    {
        public int ClientId { get; set; }
        public int RunId { get; set; }
    }
}