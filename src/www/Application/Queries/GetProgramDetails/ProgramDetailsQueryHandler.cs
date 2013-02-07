using nTestSwarm.Application.Data;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.Data.Entity;
using System.Linq;

namespace nTestSwarm.Application.Queries.GetProgramDetails
{
    public class ProgramDetailsQueryHandler : IHandler<ProgramDetailsQuery, ProgramDetailsViewModel>
    {
        private readonly nTestSwarmContext _db;

        public ProgramDetailsQueryHandler(nTestSwarmContext db)
        {
            _db = db;
        }

        public ProgramDetailsViewModel Handle(ProgramDetailsQuery request)
        {
            var lastJobId = _db.All<Job>().AsNoTracking()
                            .Where(j => j.Program.Id == request.ProgramId)
                            .Max(j => j.Id);

            //TODO: add job result 
            var viewModel = (from j in _db.All<Job>().AsNoTracking()
                             where j.Id == lastJobId
                             select new ProgramDetailsViewModel
                             {
                                 ProgramId = j.Program.Id,
                                 Name = j.Program.Name,
                                 JobDescriptionUrl = j.Program.JobDescriptionUrl,
                                 DefaultMaxRuns = j.Program.DefaultMaxRuns,
                                 LastJobCreatedTime = j.Created,
                                 LastJobStatus = j.Status.ToString(),
                                 LastCorrelation = j.Correlation
                             }).FirstOrDefault();
                        
            viewModel.UserAgents = (from p in _db.All<Program>().AsNoTracking()
                                    from u in p.UserAgentsToTest
                                    where p.Id == request.ProgramId
                                    select u)
                                    .Select(x => x.Name)
                                    .ToArray();

            return viewModel;
        }
    }
}