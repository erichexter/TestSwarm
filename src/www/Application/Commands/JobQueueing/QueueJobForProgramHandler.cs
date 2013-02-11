using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
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

        public QueueJobForProgramHandler(IDataBase db, IUserAgentCache userAgentCache, IJobDescriptionClient descriptionClient)
        {
            _db = db;
            _userAgentCache = userAgentCache;
            _descriptionClient = descriptionClient;
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
                    result.Errors.Add("url", "Url does not return expected data.");
                    return result;
                }

                var allUserAgents = _userAgentCache.GetAll();
                var job = program.AddJob(jobDescriptor.Name, correlation);

                foreach (var runDescriptor in jobDescriptor.Runs)
                {
                    var run = new Run(job, runDescriptor.Name, runDescriptor.Url);

                    if (allUserAgents != null)
                        foreach (var userAgent in allUserAgents)
                            run.RunUserAgents.Add(new RunUserAgent(run, userAgent, request.MaxRuns ?? program.DefaultMaxRuns));
                }

                _db.SaveChanges();
            }
            else
            {
                result.Errors.Add("programId", "The specified program does not exist.");
            }

            return result;
        }
    }
}