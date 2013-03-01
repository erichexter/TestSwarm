using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.DomainEventing;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public class JobCompleted : IDomainEvent
    {
        public JobCompleted()
        {
        }

        public JobCompleted(Job job)
        {
            JobId = job.Id;
        }

        public long JobId { get; set; }
    }
}