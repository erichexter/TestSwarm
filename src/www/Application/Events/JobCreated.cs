using nTestSwarm.Application.Infrastructure.DomainEventing;

namespace nTestSwarm.Application.Events
{
    public class JobCreated : IDomainEvent
    {
        public JobCreated()
        {

        }

        public JobCreated(long jobId)
        {
            JobId = jobId;
        }

        public long JobId { get; set; }
    }
}