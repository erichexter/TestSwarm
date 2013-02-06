using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace nTestSwarm.Application.Queries.UserAgentList
{
    public class UserAgentQueryHandler : IHandler<UserAgentQuery, IEnumerable<Tuple<long,string>>>
    {
        private readonly IDataBase _db;

        public UserAgentQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public IEnumerable<Tuple<long, string>> Handle(UserAgentQuery request)
        {
            return (from x in _db.All<UserAgent>().AsNoTracking()
                    select new Tuple<long, string>(x.Id, x.Name))
                    .ToArray();
        }
    }
}