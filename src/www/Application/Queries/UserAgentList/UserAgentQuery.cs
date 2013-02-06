using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System;
using System.Collections.Generic;

namespace nTestSwarm.Application.Queries.UserAgentList
{
    public class UserAgentQuery : IRequest<IEnumerable<Tuple<long,string>>>
    {
    }
}