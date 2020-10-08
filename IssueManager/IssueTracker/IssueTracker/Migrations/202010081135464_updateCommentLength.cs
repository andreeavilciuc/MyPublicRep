namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCommentLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Description", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Description", c => c.String(maxLength: 100));
        }
    }
}
