namespace nTestSwarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class missingmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "JobStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Runs", "RunStatus", c => c.Int(nullable: false));
            AddColumn("dbo.ClientRuns", "RunStatus", c => c.Int(nullable: false));
            AddColumn("dbo.RunUserAgents", "RunStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RunUserAgents", "RunStatus");
            DropColumn("dbo.ClientRuns", "RunStatus");
            DropColumn("dbo.Runs", "RunStatus");
            DropColumn("dbo.Jobs", "JobStatus");
        }
    }
}
