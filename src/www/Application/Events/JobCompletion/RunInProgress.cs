using nTestSwarm.Application.Infrastructure.DomainEventing;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public class RunInProgress : IDomainEvent
    {
        public long JobId { get; set; }
    }
}