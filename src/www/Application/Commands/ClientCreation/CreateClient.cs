using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Areas.Client.Models;

namespace nTestSwarm.Application.Commands.ClientCreation
{
    public class CreateClient : IRequest<RunViewModel>
    {
        public string OperatingSystem { get; set; }
        public string IpAddress { get; set; }
        public int Version { get; set; }
        public string Browser { get; set; }
    }
}