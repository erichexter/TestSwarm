using System.Collections.Generic;

namespace nTestSwarm.Models
{
    public class NeededClientResults
    {
        public List<NeededClient> UserAgents { get; set; }

        public List<long> Jobs { get; set; }

        public string ClientUrl { get; set; }
    }
}