using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Commands.ProgramCreation
{
    public class CreateProgram
    {
        public string Name { get; set; }
        public string JobDescriptionUrl { get; set; }
        public int? DefaultMaxRuns { get; set; }
        public long[] UserAgentIds { get; set; }
        public long[] JobIds { get; set; }
    }
}