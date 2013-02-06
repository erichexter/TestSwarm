using nTestSwarm.Application.Domain;
using System;

namespace nTestSwarm.Application.Queries.GetProgramDetails
{
    public class ProgramDetailsViewModel
    {
        public long ProgramId { get; set; }
        public string Name { get; set; }
        public string[] UserAgents { get; set; }
        public int DefaultMaxRuns { get; set; }
        public string JobDescriptionUrl { get; set; }
        public DateTime LastJobCreatedTime { get; set; }
        public string LastCorrelation { get; set; }
        public JobStatusType LastJobStatus { get; set; }
        public string LastJobResult { get; set; }
    }
}