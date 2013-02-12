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
        public string LastJobResult { get; set; }
        public Job LastJob { get; set; }
    }
}