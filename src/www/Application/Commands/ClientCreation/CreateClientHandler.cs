using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System.Linq;

namespace nTestSwarm.Application.Commands.ClientCreation
{
    public class CreateClientHandler : IHandler<CreateClient, ClientViewModel>
    {
        readonly IDataBase _db;

        public CreateClientHandler(IDataBase db)
        {
            _db = db;
        }

        public ClientViewModel Handle(CreateClient request)
        {
            var result = new ClientViewModel
            {
                Browser = request.Browser,
            };

            var userAgent = _db.All<UserAgent>()
                .Where(ua => ua.Browser.ToLower() == request.Browser.ToLower() &&
                    // support for chrome, for which we do not track versions
                    (ua.Version == null || ua.Version == request.Version))
                .FirstOrDefault();

            if (userAgent != null)
            {
                Client client = userAgent.SpawnNewClient(request.IpAddress, request.OperatingSystem);

                _db.Add(client);
                _db.SaveChanges();
                
                result.ClientId = client.Id;
                result.UserAgentName = userAgent.Name;
            }
            
            return result;
        }
    }
}