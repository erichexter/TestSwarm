using System.Data.Entity.Migrations;
using System.Linq;
using nTestSwarm.Application.Commands.JobCreation;
using nTestSwarm.Application.Data;
using nTestSwarm.Application.Repositories;

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
            var jobCreator = new CreateJobHandler(context, new UserAgentCache(() => context));
            if (!context.Jobs.Any())
            {
                jobCreator.Handle(new CreateJob
                {
                    Name = "Test Job 1",
                    Runs = new[]
                    {
                        new CreateJob.CreateNewRun
                        {
                            Name = "Passing Test",
                            Url = "http://ntestswarm-mhtest.apphb.com/tests/auto/pass/"
                        },
                        new CreateJob.CreateNewRun
                        {
                            Name = "Failing Test",
                            Url = "http://ntestswarm-mhtest.apphb.com/tests/auto/fail/"
                        },
                        new CreateJob.CreateNewRun
                        {
                            Name = "Error Test",
                            Url = "http://ntestswarm-mhtest.apphb.com/tests/auto/error/"
                        },
                        new CreateJob.CreateNewRun
                        {
                            Name = "Timeout Test",
                            Url = "http://ntestswarm-mhtest.apphb.com/tests/auto/timeout/"
                        }
                    }
                });

                context.SaveChanges();
            }
#endif
        }
    }
}