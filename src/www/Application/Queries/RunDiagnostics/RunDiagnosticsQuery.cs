using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.Collections.Generic;

namespace nTestSwarm.Application.Queries.RunDiagnostics
{
    public class RunDiagnosticsQuery : IRequest<IEnumerable<RunDiagnosticsResult>>
    {
    }
}