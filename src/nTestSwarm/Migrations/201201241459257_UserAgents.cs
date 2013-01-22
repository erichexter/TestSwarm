namespace nTestSwarm.Migrations
{
    public partial class UserAgents : nTestSwarmDbMigration
    {
        public override void Up()
        {
            AddUserAgent("Chrome", "chrome", null);
            AddUserAgent("Internet Explorer 6", "ie", 6);
            AddUserAgent("Internet Explorer 7", "ie", 7);
            AddUserAgent("Internet Explorer 8", "ie", 8);
            AddUserAgent("Internet Explorer 9", "ie", 9);
            AddUserAgent("Firefox 3", "firefox", 3);
            AddUserAgent("Firefox 8", "firefox", 8);
            AddUserAgent("Opera 9", "opera", 9);
            AddUserAgent("Safari 5", "safari", 5);
        }

        public override void Down()
        {
            Sql("delete from UserAgents");
        }
    }
}