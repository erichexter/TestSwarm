using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

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
            {
                //TODO: handle
            }

            program.Name = message.Name;
            program.JobDescriptionUrl = message.JobDescriptionUrl;
            program.DefaultMaxRuns = message.DefaultMaxRuns;

            _db.SaveChanges();
        }

    }
}