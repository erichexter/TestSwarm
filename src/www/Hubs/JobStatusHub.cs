using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace nTestSwarm.Hubs
{
    public class JobStatusHub : Hub
    {

        public Task SubscribeTo(long jobId)
        {
            return Groups.Add(Context.ConnectionId, GetGroupName(jobId));
        }

        public static async void UpdateStatus(dynamic status)
        {
            await Task.Run(() => 
            {
                var groupName = GetGroupName(status.JobId);

                GlobalHost.ConnectionManager.GetHubContext<JobStatusHub>().Clients.Group(groupName).statusChanged(status);
            });
        }

        private static string GetGroupName(long jobId)
        {
            return string.Format("JobStatus-{0}", jobId);
        }

    }
}