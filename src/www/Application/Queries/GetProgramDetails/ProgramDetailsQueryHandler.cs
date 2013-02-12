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
            var programs = _db.All<Program>().AsNoTracking();

            //TODO: add job result 
            var viewModel = (from p in programs
                             where  p.Id == request.ProgramId
                             select new ProgramDetailsViewModel
                             {
                                 ProgramId = p.Id,
                                 Name = p.Name,
                                 JobDescriptionUrl = p.JobDescriptionUrl,
                                 DefaultMaxRuns = p.DefaultMaxRuns,
                                 LastJob = (from j in p.Jobs
                                        orderby j.Id descending
                                        select j).FirstOrDefault()
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