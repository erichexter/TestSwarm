using Microsoft.AspNet.SignalR;
using System.Diagnostics;

namespace nTestSwarm.Hubs
{
    public class LastJobStatusHub : Hub
    {
        public LastJobStatusHub()
        {
            Debug.WriteLine("Hub created.");
        }

        public void Connect()
        {
            Debug.WriteLine("Connected to id {0}", (object)Context.ConnectionId);
        }
    }
}