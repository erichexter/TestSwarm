namespace nTestSwarm.Application.Queries.JobStatus
{
    public class UserAgentDto
    {
        public UserAgentDto(ResultDto result)
        {
            Browser = result.UserAgentBrowser;
            Name = result.UserAgentName;
            Id = result.UserAgentId;
        }

        public string Browser { get; set; }
        public string Name { get; set; }
        public long Id { get; set; }
    }
}