namespace nTestSwarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRunUserAgentStatus : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RunUserAgents", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RunUserAgents", "Status", c => c.Int(nullable: false));
        }
    }
}
