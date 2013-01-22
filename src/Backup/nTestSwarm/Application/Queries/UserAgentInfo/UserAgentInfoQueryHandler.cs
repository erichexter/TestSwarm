using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Queries.UserAgentInfo
{
    public class UserAgentInfoQueryHandler : IHandler<UserAgentInfoQuery, UserAgentInfoResult>
    {
        readonly IDataBase _db;

        public UserAgentInfoQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public UserAgentInfoResult Handle(UserAgentInfoQuery request)
        {
            var fiveMinutesAgo = SystemTime.NowThunk().AddMinutes(-5);

            var userAgent = _db.All<UserAgent>().Where(x => x.Id == request.Id).Select(x => x.Name).Single();

            var clients = _db.All<Client>().Where(x => x.UserAgentId == request.Id && x.Updated > fiveMinutesAgo).Select(x => new ConnectedClients
                {
                    IpAddress = x.IpAddress,
                    OperatingSystem = x.OperatingSystem,
                    FirstSeen = x.Created,
                    LastSeen = x.Updated
                });

            return new UserAgentInfoResult
                {
                    Name = userAgent,
                    ConnectedClientCount = clients.Count(),
                    Clients = clients
                };
        }
    }
}