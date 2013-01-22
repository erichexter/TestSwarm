using nTestSwarm.Application.Domain;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public class RunResult
    {
        public virtual RunUserAgentResult RunUserAgentResult { get; set; }
        public virtual string UserAgentName { get; set; }
        public virtual string RunName { get; set; }
        public virtual long RunId { get; set; }
        public virtual string Url { get; set; }

        public override string ToString()
        {
            return string.Format("{0}[{1}]:{2}", RunName, UserAgentName, RunUserAgentResult.Status.DisplayName);
        }
    }

    public class NullResult : RunResult
    {
        public override string ToString()
        {
            return "No Result";
        }
    }
}