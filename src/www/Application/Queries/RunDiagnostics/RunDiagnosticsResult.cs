using nTestSwarm.Application.Domain;
using System;

namespace nTestSwarm.Application.Queries.RunDiagnostics
{
    public class RunDiagnosticsResult
    {
        public RunStatusType Status { get; set; }
        public string RunName { get; set; }
        public string JobName { get; set; }
        public long JobId { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string OperatingSystem { get; set; }
        public DateTime Updated { get; set; }

        public int? UserAgentVersion { get; set; }
    }
}