using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Events.JobCompletion;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using nTestSwarm.Application.Services;
using System.Linq;

namespace nTestSwarm.Application.Commands.CompletedRun
{
    public class CompleteRunHandler : IHandler<CompleteRun>
    {
        readonly IDataBase _db;
        readonly IEventPublisher _eventPublisher;

        public CompleteRunHandler(IDataBase db, IEventPublisher eventPublisher)
        {
            _db = db;
            _eventPublisher = eventPublisher;
        }

        public void Handle(CompleteRun message)
        {
            var clientRun = _db.AllIncluding<ClientRun>(x => x.Client,
                                x => x.Client.UserAgent,
                                x => x.Run.Job,
                                x => x.Run.RunUserAgents.Select(r => r.UserAgent),
                                x => x.Run.ClientRuns)

                .FirstOrDefault(x => x.ClientId == message.ClientId && x.RunId == message.RunId);

            if (clientRun != null)
            {
                clientRun.Apply(message);
            }
            

            _db.SaveChanges();

            //TODO: fill in the blanks
            _eventPublisher.Publish(new RunCompleted
            {
                ClientId = message.ClientId,
                RunId = message.RunId,
                FailCount = message.Fail,
                ErrorCount = message.Error,
                TotalCount = message.Total,
                JobId=clientRun.Run.JobId
            });

            if (clientRun.Run.Job.IsComplete())
            {
                _eventPublisher.Publish(new JobCompleted()
                    {
                        JobId = clientRun.Run.JobId
                    });     
            }
        }
    }
}