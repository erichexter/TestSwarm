namespace nTestSwarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Program : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        DefaultMaxRuns = c.Int(nullable: false),
                        JobDescriptionUrl = c.String(),
                        LastJobStatus = c.String(),
                        LastJobResult = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProgramUserAgents",
                c => new
                    {
                        ProgramId = c.Long(nullable: false),
                        UserAgentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProgramId, t.UserAgentId })
                .ForeignKey("dbo.Programs", t => t.ProgramId, cascadeDelete: true)
                .ForeignKey("dbo.UserAgents", t => t.UserAgentId, cascadeDelete: true)
                .Index(t => t.ProgramId)
                .Index(t => t.UserAgentId);
            
            AddColumn("dbo.Jobs", "Program_Id", c => c.Long());
            CreateIndex("dbo.Jobs", "Program_Id");
            AddForeignKey("dbo.Jobs", "Program_Id", "dbo.Programs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "Program_Id", "dbo.Programs");
            DropForeignKey("dbo.ProgramUserAgents", "UserAgentId", "dbo.UserAgents");
            DropForeignKey("dbo.ProgramUserAgents", "ProgramId", "dbo.Programs");
            DropIndex("dbo.Jobs", new[] { "Program_Id" });
            DropIndex("dbo.ProgramUserAgents", new[] { "UserAgentId" });
            DropIndex("dbo.ProgramUserAgents", new[] { "ProgramId" });
            DropColumn("dbo.Jobs", "Program_Id");
            DropTable("dbo.ProgramUserAgents");
            DropTable("dbo.Programs");
        }
    }
}
