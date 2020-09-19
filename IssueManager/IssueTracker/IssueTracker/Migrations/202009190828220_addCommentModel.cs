namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCommentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Submitter_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "Submitter_Id");
            AddForeignKey("dbo.Comments", "Submitter_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Submitter_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "Submitter_Id" });
            DropColumn("dbo.Comments", "Submitter_Id");
        }
    }
}
