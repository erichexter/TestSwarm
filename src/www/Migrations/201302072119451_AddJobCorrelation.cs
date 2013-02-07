namespace nTestSwarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobCorrelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Correlation", c => c.String());
            DropColumn("dbo.Jobs", "JobStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "JobStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Jobs", "Correlation");
        }
    }
}
