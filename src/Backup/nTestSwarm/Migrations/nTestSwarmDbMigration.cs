using System;
using System.Data.Entity.Migrations;

namespace nTestSwarm.Migrations
{
    public abstract class nTestSwarmDbMigration : DbMigration
    {
        const string ADD_USER_AGENT =
            "insert [UserAgents]([Name], [Browser], [Version], [Created], [Updated]) values ('{0}', '{1}', {2}, '{3}', '{3}')";

        const string DROP_USER_AGENT = "delete [UserAgents] where lower([Name]) like lower('{0}')";

        public virtual void AddUserAgent(string name, string browser, int? version)
        {
            var sql = string.Format(ADD_USER_AGENT, name, browser,
                                    version.HasValue ? version.ToString() : "NULL",
                                    DateTime.Now);
            Sql(sql);
        }

        public virtual void DropUserAgentLike(string name)
        {
            var sql = string.Format(DROP_USER_AGENT, name);
            Sql(sql);
        }
    }
}