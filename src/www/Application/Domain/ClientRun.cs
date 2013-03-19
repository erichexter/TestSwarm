using nTestSwarm.Application.Commands.CompletedRun;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Domain
{
    public class ClientRun : Entity
    {
        public ClientRun() : this(null, null)
        {
        }

        public ClientRun(Client client, Run run)
        {
            Client = client;
            Run = run;
        }

        public virtual Client Client { get; protected set; }
        public long ClientId { get; protected set; }
        public virtual Run Run { get; protected set; }
        public long RunId { get; protected set; }
        public int Status { get; protected set; }
        public int FailCount { get; protected set; }
        public int ErrorCount { get; protected set; }
        public int TotalCount { get; protected set; }
        public string Results { get; protected set; }

        public RunStatusType RunStatus
        {
            get { return (RunStatusType)Status; }
            private set { Status = (int)value; }
        }

        public ClientRunStatus GetStatus()
        {
            return ClientRunStatus.FromClientRun(this);
        }

        public void RecordRunStatus(int fail, int error, int total, string results)
        {
            FailCount = fail;
            ErrorCount = error;
            TotalCount = total;
            Results = results;
            RunStatus = RunStatusType.Finished;
        }

        public string GetStatusCellContents()
        {
            return GetStatus().GetContents(this);
        }

        public void Start()
        {
            RunStatus = RunStatusType.Running;
        }

        public bool IndicatesFailureOrProblem()
        {
            return TotalCount <= 0 || ErrorCount > 0 || FailCount > 0;
        }

        public bool IndicatesSuccess()
        {
            return !IndicatesFailureOrProblem();
        }

        public void Apply(CompleteRun message)
        {
            Client.Updated = SystemTime.NowThunk();

            RecordRunStatus(fail: message.Fail,
                            error: message.Error,
                            total: message.Total,
                            results: message.Results);
            
            if (message.RepresentsAPassingRun())
            {
                Run.Pass(this);

            }
            else // test fail or error
            {
                Run.Fail(this);
            }
        }

        public void RemoveFromRun()
        {
            Run = null;
            RunId = 0;
        }

        public override string ToString()
        {
            return string.Format("status: {0}; failed: {1}; errored: {2}; total: {3}", GetStatus().DisplayName, FailCount, ErrorCount, TotalCount);
        }
    }
}