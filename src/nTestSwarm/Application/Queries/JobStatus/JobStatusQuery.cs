using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Queries.JobStatus
{
    public class JobStatusQuery : IRequest<JobStatusResult>
    {
        public JobStatusQuery(long id)
        {
            JobId = id;
        }

        public long JobId { get; set; }
    }
}