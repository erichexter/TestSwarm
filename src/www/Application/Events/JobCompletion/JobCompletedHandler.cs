using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Hubs;

namespace nTestSwarm.Application.Events.JobCompletion
{
    public class JobComleteHandlerNotifier : IHandler<JobCompleted>
    {
        public void Handle(JobCompleted message)
        {
            JobStatusHub.JobFinished(message.JobId);
        }
    }
}