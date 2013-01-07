using nTestSwarm.Application.Domain;

namespace nTestSwarm.Application.Queries.JobStatus
{
    public class ResultCellDto
    {
        public ResultCellDto(ResultDto result)
        {
            Status = Enumeration.FromValue<ClientRunStatus>(result.StatusValue);
            ClientId = result.ClientId;
            CellContents = result.CellContents;
            UserAgentBrowser = result.UserAgentBrowser;
            UserAgentName = result.UserAgentName;
            UserAgentVersion = result.UserAgentVersion;
        }

        public long? ClientId { get; set; }
        public string CellContents { get; set; }
        public string UserAgentBrowser { get; set; }
        public string UserAgentName { get; set; }
        public int? UserAgentVersion { get; set; }
        public ClientRunStatus Status { get; set; }
    }
}