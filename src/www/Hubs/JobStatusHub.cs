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

        public Task<JobStatusResult> SubscribeTo(long jobId)
        {
            return Groups
                    .Add(Context.ConnectionId, GetGroupName(jobId))
                    .ContinueWith(_ => _bus.Request(new JobStatusQuery(jobId)).Data);
        }

        public static async void UpdateStatus(JobStatusResult status)
        {
            await Task.Run(() => 
            {
                var groupName = GetGroupName(status.JobId);

                GlobalHost.ConnectionManager.GetHubContext<JobStatusHub>().Clients.Group(groupName).statusChanged(status.RunResults);
            });
        }

        private static string GetGroupName(long jobId)
        {
            return string.Format("JobStatus-{0}", jobId);
        }

    }
}