using System.ComponentModel.DataAnnotations.Schema;

namespace nTestSwarm.Application.Domain
{
    public class RunUserAgent : Entity
    {
        public RunUserAgent(Run run, UserAgent userAgent, int? maxRuns = 1)
        {
            Run = run;
            UserAgent = userAgent;
            RunStatus = RunStatusType.NotStarted;

            RemainingRuns = MaxRuns = maxRuns.GetValueOrDefault();
            Result = new RunUserAgentResult();
        }

        public RunUserAgent() : this(null, null, null)
        {
        }

        public virtual UserAgent UserAgent { get; protected set; }
        public long UserAgentId { get; protected set; }

        public virtual Run Run { get; protected set; }
        public long RunId { get; protected set; }

        public int MaxRuns { get; protected set; }
        public int RemainingRuns { get; protected set; }
        public long? ActiveClientId { get; protected set; }
        public RunUserAgentResult Result { get; protected set; }

        public RunStatusType RunStatus { get; protected set; }

        public void Pass(ClientRun clientRun)
        {
            RemainingRuns = 0;
            ActiveClientId = null;
            RunStatus = RunStatusType.Finished;

            Result = new RunUserAgentResult(clientRun);
        }

        public void Fail(ClientRun clientRun)
        {
            if (HasPassed())
                return;

            RemainingRuns--;
            ActiveClientId = null;
            RunStatus = RemainingRuns > 0 ? RunStatusType.Running : RunStatusType.Finished;

            Result = new RunUserAgentResult(clientRun);
        }

        public void Reset()
        {
            ActiveClientId = null;
            RunStatus = RunStatusType.NotStarted;
            RemainingRuns = MaxRuns;
            Result = new RunUserAgentResult();
        }

        public bool HasPassed()
        {
            return RunStatus == RunStatusType.Finished 
                   && Result != null
                   && Result.TotalTests.HasValue && Result.TotalTests > 0
                   && (!Result.FailedTests.HasValue || Result.FailedTests == 0);
        }

        public bool IsFinished()
        {
            return RunStatus == RunStatusType.Finished;
        }

        public void StartOrContinue(Client client)
        {
            ActiveClientId = client.Id;
            RunStatus = RunStatusType.Running;
            Result.InProgress();
        }
    }
}