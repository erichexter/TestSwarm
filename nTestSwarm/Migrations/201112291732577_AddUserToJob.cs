namespace nTestSwarm.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("Jobs", "Private", c => c.Boolean(nullable: false));
            AddColumn("Jobs", "User_Id", c => c.Long());
            AddForeignKey("Jobs", "User_Id", "Users", "Id");
            CreateIndex("Jobs", "User_Id");
        }
        
        public override void Down()
        {
            DropIndex("Jobs", new[] { "User_Id" });
            DropForeignKey("Jobs", "User_Id", "Users", "Id");
            DropColumn("Jobs", "User_Id");
            DropColumn("Jobs", "Private");
        }
    }
}
