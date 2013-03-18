using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace nTestSwarm.Application.Queries.UserAgentList
{
    public class UserAgentQueryHandler : IHandler<UserAgentQuery, IEnumerable<Descriptor>>
    {
        private readonly IDataBase _db;

        public UserAgentQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public IEnumerable<Descriptor> Handle(UserAgentQuery request)
        {
            return _db.All<UserAgent>().AsNoTracking()
                        .ToDescriptors()
                        .ToArray();
        }
    }
}