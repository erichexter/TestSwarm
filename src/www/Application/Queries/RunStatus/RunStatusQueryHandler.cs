using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Queries.RunStatus
{
    public class RunStatusQueryHandler : IHandler<RunStatusQuery, RunStatusResult>
    {
        readonly IDataBase _dataBase;

        public RunStatusQueryHandler(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }

        public RunStatusResult Handle(RunStatusQuery request)
        {
            var status = from run in _dataBase.All<ClientRun>()
                         where run.Client.Id == request.ClientId && run.Run.Id == request.RunId
                         select run.Results;

            return new RunStatusResult
                {
                    Results =
                        status.FirstOrDefault() ??
                        string.Format("no run status for client {0} and run {1}", request.ClientId, request.RunId)
                };
        }
    }
}