using System.ComponentModel.DataAnnotations;

namespace nTestSwarm.Application.Commands
{
    public class ProgramCommand
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string JobDescriptionUrl { get; set; }

        public int? DefaultMaxRuns { get; set; }

        public long[] UserAgentIds { get; set; }
    }
}