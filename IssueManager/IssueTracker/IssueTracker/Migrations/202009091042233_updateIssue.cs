namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateIssue : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Issues", "SubmitterID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "SubmitterID", c => c.Int(nullable: false));
        }
    }
}
