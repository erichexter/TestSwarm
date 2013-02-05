using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace www.Application.Commands.JobQueueing
{
    public class QueueJobForProgram : IRequest<QueueJobForProgramResult>
    {
        public int ProgramId { get; set; }
        public string[] Correlation { get; set; }
        public string Url { get; set; }
        public int? MaxRuns { get; set; }
    }
}