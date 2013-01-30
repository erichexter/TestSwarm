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
                        Name = c.String(nullable: false),
                        DefaultMaxRuns = c.Int(nullable: false),
                        JobDescriptionUrl = c.String(),
                        LastJobStatus = c.String(),
                        LastJobResult = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserAgents", "Program_Id", c => c.Long());
            AddColumn("dbo.Jobs", "Program_Id", c => c.Long());
            CreateIndex("dbo.UserAgents", "Program_Id");
            CreateIndex("dbo.Jobs", "Program_Id");
            AddForeignKey("dbo.UserAgents", "Program_Id", "dbo.Programs", "Id");
            AddForeignKey("dbo.Jobs", "Program_Id", "dbo.Programs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "Program_Id", "dbo.Programs");
            DropForeignKey("dbo.UserAgents", "Program_Id", "dbo.Programs");
            DropIndex("dbo.Jobs", new[] { "Program_Id" });
            DropIndex("dbo.UserAgents", new[] { "Program_Id" });
            DropColumn("dbo.Jobs", "Program_Id");
            DropColumn("dbo.UserAgents", "Program_Id");
            DropTable("dbo.Programs");
        }
    }
}
