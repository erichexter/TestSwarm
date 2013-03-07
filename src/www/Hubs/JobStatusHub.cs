using Microsoft.AspNet.SignalR;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobStatus;
using System.Threading.Tasks;

namespace nTestSwarm.Hubs
{
    public class JobStatusHub : Hub
    {

        private readonly IBus _bus;

        public JobStatusHub(IBus bus)
        {
            _bus = bus;
        }

        private static dynamic AllClients
        {
            get { return GlobalHost.ConnectionManager.GetHubContext<JobStatusHub>().Clients.All; }
        }

        public Task<JobStatusResult> SubscribeTo(long jobId)
        {
            return Groups
                    .Add(Context.ConnectionId, GetGroupName(jobId))
                    .ContinueWith(_ => _bus.Request(new JobStatusQuery(jobId)).Data);
        }

        public static async void UpdateStatus(JobStatusResult status)
        {
            await GetJobClients(status.JobId).statusChanged(status);
        }

        public static async void JobStarted(long id)
        {
            await AllClients.started(id);
        }

        public static async void JobFinished(long id)
        {
            await AllClients.finished(id);
        }

        private static dynamic GetJobClients(long jobId)
        {
            var groupName = GetGroupName(jobId);

            return GlobalHost.ConnectionManager.GetHubContext<JobStatusHub>().Clients.Group(groupName);
        }

        private static string GetGroupName(long jobId)
        {
            return string.Format("JobStatus-{0}", jobId);
        }

    }
}