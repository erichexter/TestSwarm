namespace nTestSwarm.Application.Queries.JobStatus
{
    public class ResultDto
    {
        public long RunId { get; set; }
        public string RunName { get; set; }
        public string RunUrl { get; set; }
        public long JobId { get; set; }
        public string JobName { get; set; }

        public long UserAgentId { get; set; }
        public string UserAgentBrowser { get; set; }
        public string UserAgentName { get; set; }
        public int? UserAgentVersion { get; set; }
        public long? ClientId { get; set; }
        public string CellContents { get; set; }
        public int StatusValue { get; set; }
    }
}