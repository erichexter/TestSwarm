using System.Data.Entity;
using System.Data.Entity.Infrastructure;

[assembly: WebActivator.PreApplicationStartMethod(typeof(nTestSwarmTests.App_Start.EntityFramework_SqlServerCompact), "Start")]

namespace nTestSwarmTests.App_Start {
    public static class EntityFramework_SqlServerCompact {
        public static void Start() {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            if (Database.Exists("nTestSwarmContext.sdf"))
            {
                Database.Delete("nTestSwarmContext.sdf");
            }
        }
    }
}
