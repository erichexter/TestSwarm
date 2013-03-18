using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.DomainEventing;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public class JobCompleted : IDomainEvent
    {
        public JobCompleted(Job job)
        {
            JobId = job.Id;
        }

        public JobCompleted(long jobId)
        {
            JobId = jobId;
        }

        public long JobId { get; set; }
    }
}