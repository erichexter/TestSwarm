using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Commands.JobCreation.Described
{
    public class CreateJobFromDescription : IRequest<CreateJobResult>
    {
        public string[] Correlation { get; set; }
        public string Url { get; set; }
        public int? MaxRuns { get; set; }

        public string ApiKey { get; set; }

        public bool Private { get; set; }
    }
}