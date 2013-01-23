using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Queries.UserAgentInfo
{
    public class UserAgentInfoQuery : IRequest<UserAgentInfoResult>
    {
        public long Id { get; set; }
    }
}