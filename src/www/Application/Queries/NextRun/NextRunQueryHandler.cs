using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Events.JobCompletion;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using nTestSwarm.Application.Queries.NextRun;
using nTestSwarm.Application.Services;
using System;

namespace nTestSwarm.Application.NextRun
{
    public class NextRunQueryHandler : IHandler<NextRunQuery, NextRunResult>
    {
        readonly IDataBase _db;
        readonly IRunQueue _queue;
        readonly IEventPublisher _eventPublisher;

        public NextRunQueryHandler(IDataBase db, IRunQueue queue, IEventPublisher eventPublisher)
        {
            _db = db;
            _queue = queue;
            _eventPublisher = eventPublisher;
        }

        public NextRunResult Handle(NextRunQuery request)
        {
            var client = _db.Find<Client>(request.ClientId);

            if (client == null)
                throw new Exception("Client doesn't exist");

            var nextRun = _queue.GetNext(client);

            if (nextRun == null)
                return null;

            _eventPublisher.Publish(new RunInProgress(nextRun.JobId, nextRun.Id));

            return new NextRunResult(nextRun);
        }
    }
}