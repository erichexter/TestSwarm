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
            Job = job.Id;
        }

        public long Job { get; set; }
    }
}