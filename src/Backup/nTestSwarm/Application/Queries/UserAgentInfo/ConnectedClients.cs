using System;

namespace nTestSwarm.Application.Queries.UserAgentInfo
{
    public class ConnectedClients
    {
        public string IpAddress { get; set; }
        public string OperatingSystem { get; set; }
        public DateTime LastSeen { get; set; }
        public DateTime FirstSeen { get; set; }
    }
}