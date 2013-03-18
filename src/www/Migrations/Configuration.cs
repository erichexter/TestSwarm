using nTestSwarm.Application.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using ProgramEntity = nTestSwarm.Application.Domain.Program;

namespace nTestSwarm.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<nTestSwarmContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            CodeGenerator = new nTestSwarmMigrationCodeGenerator();
        }

        protected override void Seed(nTestSwarmContext context)
        {
#if DEBUG
            SeedPrograms(context);
#endif
        }

        private void SeedPrograms(nTestSwarmContext context)
        {
            const string seedProgramName = "localhost test";

            var program = new ProgramEntity(seedProgramName, "http://localhost:38597/functionaltests/runner/list", 2);
            var userAgents = context.UserAgents
                                .Where(x => x.Name.StartsWith("Chrome"))
                                .ToList();

            userAgents.ForEach(program.UserAgentsToTest.Add);
            context.Programs.AddOrUpdate(p => p.Name, program);
            context.SaveChanges();
        }

    }
}