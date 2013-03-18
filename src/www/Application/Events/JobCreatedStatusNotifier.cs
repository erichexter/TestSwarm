using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Hubs;

namespace nTestSwarm.Application.Events
{
    public class JobCreatedStatusNotifier : IHandler<JobCreated>
    {
        public void Handle(JobCreated message)
        {
            JobStatusHub.JobStarted(message.JobId);
        }
    }
}