namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedEmailToUsername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Issues", "ApplicationUser_Id");
            AddForeignKey("dbo.Issues", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Issues", "ApplicationUser_Id");
        }
    }
}
