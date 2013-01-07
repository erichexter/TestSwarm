using System.Collections.Generic;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Commands.JobCreation
{
    public class CreateJob : IRequest<CreateJobResult>
    {
        public string Name { get; set; }
        public int? MaxRuns { get; set; }
        public IEnumerable<CreateNewRun> Runs { get; set; }

        public string SuiteId { get; set; }

        public string ApiKey { get; set; }

        public bool Private { get; set; }

        public class CreateNewRun
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }
    }
}