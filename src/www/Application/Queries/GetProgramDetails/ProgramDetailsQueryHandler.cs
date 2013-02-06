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
            //TODO: add job result and correlation values
            const string sql = "SELECT " +
                                    "p.Id ProgramId, " +
                                    "p.Name, " +
                                    "p.DefaultMaxRuns, " +
                                    "p.JobDescriptionUrl, " +
                                    "j.Created LastJobCreatedTime, " +
                                    "j.Status LastJobStatus " +
                                "FROM " +
                                    "Programs p " +
                                    "RIGHT JOIN Jobs j ON p.Id = j.Program_Id " +
                                "WHERE " +
                                    "p.ID = @p0 " +
                                    "AND j.ID = (SELECT Max(Id) FROM jobs)";

            var viewModel = _db.Database
                              .SqlQuery<ProgramDetailsViewModel>(sql, new object[] { request.ProgramId })
                              .FirstOrDefault();

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