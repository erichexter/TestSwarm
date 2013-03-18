using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;
using System;
using System.Linq;

namespace nTestSwarm.Application.Commands.ProgramUpdate
{
    public class UpdateProgramHandler : IHandler<UpdateProgram>
    {

        private readonly IDataBase _db;

        public UpdateProgramHandler(IDataBase db)
        {
            _db = db;
        }

        public void Handle(UpdateProgram message)
        {
            var program = _db.Find<Program>(message.ProgramId);

            if (program == null)
                throw new Exception("program does not exist");

            program.Name = message.Name;
            program.JobDescriptionUrl = message.JobDescriptionUrl;
            program.DefaultMaxRuns = message.DefaultMaxRuns ?? 10;
            program.UserAgentsToTest.Clear();

            _db.All<UserAgent>()
                .Where(f => message.UserAgentIds.Any(t => t == f.Id))
                .Each(program.UserAgentsToTest.Add);

            _db.SaveChanges();
        }

    }
}