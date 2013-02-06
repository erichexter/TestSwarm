using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace nTestSwarm.Application.Queries.GetProgram
{
    public class ProgramQueryHandler : IHandler<ProgramQuery, ProgramViewModel>
    {
        private readonly IDataBase _db;

        public ProgramQueryHandler(IDataBase db)
        {
            _db = db;
        }

        public ProgramViewModel Handle(ProgramQuery request)
        {
            var allUserAgents = _db.All<UserAgent>().AsNoTracking();

            return (from p in _db.All<Program>().AsNoTracking()
                    where p.Id == request.ProgramId
                    select new ProgramViewModel
                    {
                        ProgramId = p.Id,
                        Name = p.Name,
                        JobDescriptionUrl = p.JobDescriptionUrl,
                        DefaultMaxRuns = p.DefaultMaxRuns,
                        UserAgents = allUserAgents.Select(x => new SelectListItem
                        {
                            Value = x.Id.ToString(),
                            Text = x.Name,
                            Selected = p.UserAgentsToTest.Any(userAgent => userAgent.Id == x.Id)
                        }).ToArray()
                    })
                    .FirstOrDefault();
        }
    }
}