using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Queries.UserLookup
{
    public class UserQueryHandler:IHandler<UserQuery,User>
    {
        private readonly IDataBase _db;

        public UserQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public User Handle(UserQuery request)
        {
            return _db.All<User>().FirstOrDefault(u => u.Username == request.Username);
        }
    }
}