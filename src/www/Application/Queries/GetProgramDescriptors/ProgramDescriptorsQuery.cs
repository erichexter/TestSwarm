using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.Collections.Generic;

namespace nTestSwarm.Application.Queries.GetProgramDescriptors
{
    public class ProgramDescriptorsQuery : IRequest<IEnumerable<Descriptor>>
    {
    }
}