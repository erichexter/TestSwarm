using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System.Linq;

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
            var program = _db.Find<Program>(request.ProgramId);
            var programUserAgents = _db.All<UserAgent>()
                                        .ToDescriptors()
                                        .ToArray()
                                        .SelectedBy(d => program.UserAgentsToTest.Any(ua => ua.Id == d.Id));

            return new ProgramViewModel(programUserAgents)
            {
                ProgramId = program.Id,
                Name = program.Name,
                JobDescriptionUrl = program.JobDescriptionUrl,
                DefaultMaxRuns = program.DefaultMaxRuns
            };
        }
    }
}