using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Queries.UserLookup
{
    public class UserQuery:IRequest<User>
    {
        public string Username { get; set; }    
    }
}