namespace nTestSwarm.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Username = c.String(),
                        Name = c.String(),
                        EmailAddress = c.String(),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                        ApiKey = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "UserAgents",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Browser = c.String(),
                        Version = c.Int(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Clients",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserAgentId = c.Long(nullable: false),
                        IpAddress = c.String(),
                        OperatingSystem = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("UserAgents", t => t.UserAgentId, cascadeDelete: true)
                .Index(t => t.UserAgentId);
            
            CreateTable(
                "Jobs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Started = c.DateTime(),
                        Finished = c.DateTime(),
                        SuiteID = c.String(),
                        Status = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Runs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        JobId = c.Long(nullable: false),
                        Name = c.String(),
                        Url = c.String(),
                        Status = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
            CreateTable(
                "ClientRuns",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClientId = c.Long(nullable: false),
                        RunId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        FailCount = c.Int(nullable: false),
                        ErrorCount = c.Int(nullable: false),
                        TotalCount = c.Int(nullable: false),
                        Results = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Runs", t => t.RunId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.RunId);
            
            CreateTable(
                "RunUserAgents",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserAgentId = c.Long(nullable: false),
                        RunId = c.Long(nullable: false),
                        MaxRuns = c.Int(nullable: false),
                        RemainingRuns = c.Int(nullable: false),
                        ActiveClientId = c.Long(),
                        Status = c.Int(nullable: false),
                        Result_StatusValue = c.Int(nullable: false),
                        Result_ClientId = c.Long(),
                        Result_CellContents = c.String(),
                        Result_TotalTests = c.Int(),
                        Result_FailedTests = c.Int(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("UserAgents", t => t.UserAgentId, cascadeDelete: true)
                .ForeignKey("Runs", t => t.RunId, cascadeDelete: true)
                .Index(t => t.UserAgentId)
                .Index(t => t.RunId);
            
            CreateTable(
                "RunUserAgentCompareResults",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SourceJobId = c.Long(nullable: false),
                        SourceJobName = c.String(),
                        TargetJobId = c.Long(nullable: false),
                        TargetJobName = c.String(),
                        RunName = c.String(),
                        TargetRunUrl = c.String(),
                        UserAgentName = c.String(),
                        TransitionValue = c.Int(nullable: false),
                        ClientId = c.Long(),
                        RunId = c.Long(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Events",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Processed = c.DateTime(),
                        SerializedData = c.String(),
                        Type = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("RunUserAgents", new[] { "RunId" });
            DropIndex("RunUserAgents", new[] { "UserAgentId" });
            DropIndex("ClientRuns", new[] { "RunId" });
            DropIndex("ClientRuns", new[] { "ClientId" });
            DropIndex("Runs", new[] { "JobId" });
            DropIndex("Clients", new[] { "UserAgentId" });
            DropForeignKey("RunUserAgents", "RunId", "Runs");
            DropForeignKey("RunUserAgents", "UserAgentId", "UserAgents");
            DropForeignKey("ClientRuns", "RunId", "Runs");
            DropForeignKey("ClientRuns", "ClientId", "Clients");
            DropForeignKey("Runs", "JobId", "Jobs");
            DropForeignKey("Clients", "UserAgentId", "UserAgents");
            DropTable("Events");
            DropTable("RunUserAgentCompareResults");
            DropTable("RunUserAgents");
            DropTable("ClientRuns");
            DropTable("Runs");
            DropTable("Jobs");
            DropTable("Clients");
            DropTable("UserAgents");
            DropTable("Users");
        }
    }
}
