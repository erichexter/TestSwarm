using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Queries.RunStatus
{
    public class UserExistsQueryHandler : IHandler<UserExistsQuery, UserExistsResult>
    {
        readonly IDataBase _dataBase;

        public UserExistsQueryHandler(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }

        public UserExistsResult Handle(UserExistsQuery request)
        {
            var exists = _dataBase.All<User>()
                .Any(user=> user.Id != request.Id 
                    && user.Username == request.Username);

            return new UserExistsResult()
                {
                    Exists = exists
                };
        }
    }
}