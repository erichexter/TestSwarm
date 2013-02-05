using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Commands.ProgramCreation
{
    public class CreateProgramHandler : IHandler<CreateProgram>
    {
        private readonly IDataBase _db;

        public CreateProgramHandler(IDataBase db)
        {
            _db = db;
        }

        public void Handle(CreateProgram message)
        {
            var program = new Program(message.Name, message.JobDescriptionUrl, message.DefaultMaxRuns ?? 10);

            _db.Add(program);
            _db.SaveChanges();
        }
    }
}