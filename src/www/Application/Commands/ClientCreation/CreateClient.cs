using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Commands.ClientCreation
{
    public class CreateClient : IRequest<ClientViewModel>
    {
        public string OperatingSystem { get; set; }
        public string IpAddress { get; set; }
        public int Version { get; set; }
        public string Browser { get; set; }
    }
}