namespace nTestSwarm.Application.Domain
{
    public class UserAgent : Entity
    {
        public UserAgent() : this(null, null, null)
        {
        }

        public UserAgent(string browser, string name, int? version)
        {
            Browser = browser;
            Name = name;
            Version = version;
        }

        public string Name { get; protected set; }
        public string Browser { get; protected set; }
        public int? Version { get; protected set; }

        public Client SpawnNewClient(string ipAddress, string operatingSystem)
        {
            return new Client(ipAddress, operatingSystem, this);
        }
    }
}