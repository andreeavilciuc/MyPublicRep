namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateIssueModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "Subject", c => c.String(maxLength: 50));
            AddColumn("dbo.Issues", "Content", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "Content");
            DropColumn("dbo.Issues", "Subject");
        }
    }
}
