using System.Data.Entity;
using WebActivator;
using nTestSwarm.App_Start;
using nTestSwarm.Application.Data;
using nTestSwarm.Migrations;

[assembly: PreApplicationStartMethod(typeof (AutoMigration), "Start")]

namespace nTestSwarm.App_Start
{
    public static class AutoMigration
    {
        public static void Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<nTestSwarmContext, Configuration>());
        }
    }
}