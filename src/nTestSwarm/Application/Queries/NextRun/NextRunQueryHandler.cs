using System;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.NextRun;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.NextRun
{
    public class NextRunQueryHandler : IHandler<NextRunQuery, NextRunResult>
    {
        readonly IDataBase _db;
        readonly IRunQueue _queue;

        public NextRunQueryHandler(IDataBase db, IRunQueue queue)
        {
            _db = db;
            _queue = queue;
        }

        public NextRunResult Handle(NextRunQuery request)
        {
            var client = _db.Find<Client>(request.ClientId);

            if (client == null)
                throw new Exception("Client doesn't exist");

            var nextRun = _queue.GetNext(client);

            return nextRun == null ? null : new NextRunResult(nextRun);
        }
    }
}