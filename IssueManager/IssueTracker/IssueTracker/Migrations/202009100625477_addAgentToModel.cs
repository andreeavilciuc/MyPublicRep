namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAgentToModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "Agent_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Issues", "Agent_Id");
            AddForeignKey("dbo.Issues", "Agent_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "Agent_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "Agent_Id" });
            DropColumn("dbo.Issues", "Agent_Id");
        }
    }
}
