namespace nTestSwarm.Migrations
{
    public partial class RemoveOperaAndSafari : nTestSwarmDbMigration
    {
        public override void Up()
        {
            DropUserAgentLike("Safari%");
            DropUserAgentLike("Opera%");
        }

        public override void Down()
        {
        }
    }
}