using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Events;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using nTestSwarm.Application.Repositories;
using nTestSwarm.Application.Services;
using System;

namespace nTestSwarm.Application.Commands.JobQueueing
{
    public class QueueJobForProgramHandler : IHandler<QueueJobForProgram, QueueJobForProgramResult>
    {
        private readonly IDataBase _db;
        private readonly IUserAgentCache _userAgentCache;
        private readonly IJobDescriptionClient _descriptionClient;
        private readonly IEventPublisher _eventPublisher;

        public QueueJobForProgramHandler(IDataBase db, IUserAgentCache userAgentCache, IJobDescriptionClient descriptionClient, IEventPublisher eventPublisher)
        {
            _db = db;
            _userAgentCache = userAgentCache;
            _descriptionClient = descriptionClient;
            _eventPublisher = eventPublisher;
        }

        public QueueJobForProgramResult Handle(QueueJobForProgram request)
        {
            var result = new QueueJobForProgramResult();
            var program = _db.Find<Program>(request.ProgramId);

            if (program != null)
            {
                var correlation = request.Correlation.ReplaceNullOrWhitespace(() => DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                var jobDescriptionUrl = request.Url.ReplaceNullOrWhitespace(program.JobDescriptionUrl);
                var jobDescriptor = _descriptionClient.GetFrom(jobDescriptionUrl, new[] { correlation });

                if (jobDescriptor == null || string.IsNullOrWhiteSpace(jobDescriptor.Name))
                {
                    result.Errors.Add("url", "Job description url does not return the expected data.");
                    return result;
                }

                var job = program.AddJob(jobDescriptor.Name, correlation);

                // create a run for each run (test url) specified by the job description url
                foreach (var runDescriptor in jobDescriptor.Runs)
                {
                    var run = new Run(job, runDescriptor.Name, runDescriptor.Url);
                    job.Runs.Add(run);

                    // create a RunUserAgent for each user agent specified by the run's program
                    if (program.UserAgentsToTest != null)
                        foreach (var userAgent in program.UserAgentsToTest)
                            run.RunUserAgents.Add(new RunUserAgent(run, userAgent, request.MaxRuns ?? program.DefaultMaxRuns));
                }

                _db.SaveChanges();
                _eventPublisher.Publish(new JobCreated(job.Id));
            }
            else
            {
                result.Errors.Add("programId", "The specified program does not exist.");
            }

            return result;
        }
    }
}