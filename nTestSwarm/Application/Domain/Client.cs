using System.ComponentModel.DataAnnotations;

namespace nTestSwarm.Application.Domain
{
    public class Client : Entity
    {
        public Client()
        {
        }

        public Client(UserAgent userAgent) : this(null, null, userAgent)
        {
        }

        public Client(string ipAddress, string operatingSystem, UserAgent userAgent)
        {
            IpAddress = ipAddress;
            OperatingSystem = operatingSystem;
            UserAgent = userAgent;
        }

        public long UserAgentId { get; protected set; }
        public virtual UserAgent UserAgent { get; protected set; }
        public string IpAddress { get; protected set; }
        public string OperatingSystem { get; protected set; }
    }
}