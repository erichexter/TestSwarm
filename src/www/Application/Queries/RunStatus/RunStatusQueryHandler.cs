using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System.Data.Entity;
using System.Linq;

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
            var status = (from run in _dataBase.All<ClientRun>().AsNoTracking()
                          where run.Client.Id == request.ClientId && run.Run.Id == request.RunId
                          select run).FirstOrDefault();

            if (status == null)
                return new RunStatusResult { Results = string.Format("no run status for client {0} and run {1}", request.ClientId, request.RunId) };
            else
                return new RunStatusResult
                {
                    Results = status.Results ?? status.ToString()
                };
        }
    }
}