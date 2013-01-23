using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Queries.RunStatus
{
    public class UserExistsQuery : IRequest<UserExistsResult>
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}