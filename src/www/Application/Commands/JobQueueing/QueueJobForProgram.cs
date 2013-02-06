using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.ComponentModel.DataAnnotations;

namespace www.Application.Commands.JobQueueing
{
    public class QueueJobForProgram : IRequest<QueueJobForProgramResult>
    {
        [Required]
        public int ProgramId { get; set; }

        [Required]
        public string Url { get; set; }

        public string Correlation { get; set; }
        
        public int? MaxRuns { get; set; }
    }
}