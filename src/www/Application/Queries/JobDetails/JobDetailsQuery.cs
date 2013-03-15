using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Models;

namespace nTestSwarm.Application.Queries.JobDetails
{
    public class JobDetailsQuery : IRequest<JobDetailsViewModel>
    {
        public long Id { get; set; }
    }
}