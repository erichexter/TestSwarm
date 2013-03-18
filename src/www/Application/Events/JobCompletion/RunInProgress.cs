using nTestSwarm.Application.Infrastructure.DomainEventing;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public class RunInProgress : IDomainEvent
    {
        public RunInProgress(long jobId, long runId)
        {
            JobId = JobId;
            RunId = runId;
        }

        public long JobId { get; private set; }
        public long RunId { get; private set; }
    }
}