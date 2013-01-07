using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Queries.JobSummary
{
    public class JobSummaryQuery : IRequest<JobSummaryResult>
    {
        public JobSummaryQuery(long id)
        {
            JobId = id;
        }

        public long JobId { get; set; }
    }
}