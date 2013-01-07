using System;
using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Repositories;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Commands.JobCreation
{
    public class CreateJobHandler : IHandler<CreateJob, CreateJobResult>
    {
        public const int DefaultMaxRuns = 2;

        readonly IDataBase _db;
        readonly IUserAgentCache _userAgentCache;

        public CreateJobHandler(IDataBase db, IUserAgentCache userAgentCache)
        {
            _db = db;
            _userAgentCache = userAgentCache;
        }

        public CreateJobResult Handle(CreateJob request)
        {
            var job = new Job(request.Name);
            job.SuiteID = request.SuiteId;
            job.Private = request.Private;
            var allUserAgents = _userAgentCache.GetAll();

            Guid apikey;
            if(request.ApiKey.HasValue() && Guid.TryParse(request.ApiKey,out apikey))
            {
                var user = _db.All<User>().FirstOrDefault(u => u.ApiKey == apikey);
                if(user !=null)
                {
                    job.User = user;
                }
            }
            foreach (var runDesc in request.Runs)
            {
                var run = new Run(job, runDesc.Name, runDesc.Url);
                
                if (allUserAgents != null)
                foreach (var userAgent in allUserAgents)
                {
                    var runUserAgent = new RunUserAgent(run, userAgent, request.MaxRuns ?? DefaultMaxRuns);
                    _db.Add(runUserAgent);
                }
            }
            _db.SaveChanges();

            return new CreateJobResult(job);

        }
    }
}