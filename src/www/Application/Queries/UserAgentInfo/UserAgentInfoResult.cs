using System.Collections.Generic;

namespace nTestSwarm.Application.Queries.UserAgentInfo
{
    public class UserAgentInfoResult
    {
        public string Name { get; set; }
        public IEnumerable<ConnectedClients> Clients { get; set; }
        public int ConnectedClientCount { get; set; }
    }
}