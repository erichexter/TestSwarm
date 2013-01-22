using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nTestSwarm.Application.Domain
{
    [ComplexType]
    public class RunUserAgentResult
    {
        public RunUserAgentResult() : this(null)
        {
        }

        public RunUserAgentResult(ClientRun clientRun)
        {
            if (clientRun == null)
            {
                ClientId = null;
                Status = ClientRunStatus.NotStarted;
                CellContents = string.Empty;
                TotalTests = null;
                FailedTests = null;
            }
            else
            {
                ClientId = clientRun.Client.Id;
                Status = clientRun.GetStatus();
                CellContents = clientRun.GetStatusCellContents();
                SetCounts(clientRun);
            }
        }

        public int StatusValue { get; protected set; }

        [NotMapped]
        public ClientRunStatus Status
        {
            get { return (ClientRunStatus)Enumeration.FromValueOrDefault(typeof(ClientRunStatus), StatusValue); }
            protected set { StatusValue = value.Value; }
        }

        public long? ClientId { get; protected set; }
        public string CellContents { get; protected set; }

        public int? TotalTests { get; protected set; }
        public int? FailedTests { get; protected set; }

        void SetCounts(ClientRun clientRun)
        {
            // counts for rollups are only based on failed or passed runs
            if (clientRun.GetStatus() == ClientRunStatus.Fail || clientRun.GetStatus() == ClientRunStatus.Pass)
            {
                TotalTests = clientRun.TotalCount;
                FailedTests = clientRun.FailCount;
            }
        }

        public void InProgress()
        {
            Status = ClientRunStatus.InProgress;
            TotalTests = null;
            FailedTests = null;
        }
    }
}