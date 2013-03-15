using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nTestSwarm.Application.Commands.JobCreation
{
    public class CreateJob : IRequest<CreateJobResult>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SuiteId { get; set; }

        public int? MaxRuns { get; set; }
        
        public IEnumerable<CreateNewRun> Runs { get; set; }

        public class CreateNewRun
        {
            [Required]
            public string Name { get; set; }
            
            [Required]
            public string Url { get; set; }
        }
    }
}