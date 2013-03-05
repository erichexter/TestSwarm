using nTestSwarm.Application.Infrastructure.DomainEventing;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public class RunCompleted : IDomainEvent
    {
        public long RunId { get; set; }
        public long ClientId { get; set; }
        public long JobId { get; set; }
        public int FailCount { get; set; }
        public int ErrorCount { get; set; }
        public int TotalCount { get; set; }
        public string Results { get; set; }
    }

    public class RunInProgress : IDomainEvent
    {
        public long JobId { get; set; }
    }
}