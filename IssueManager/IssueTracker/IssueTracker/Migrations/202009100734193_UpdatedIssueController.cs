namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedIssueController : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Issues", name: "Agent_Id", newName: "AssignedTo_Id");
            RenameIndex(table: "dbo.Issues", name: "IX_Agent_Id", newName: "IX_AssignedTo_Id");
            AddColumn("dbo.Issues", "LastChangedBy_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Issues", "LastChangedBy_Id");
            AddForeignKey("dbo.Issues", "LastChangedBy_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "LastChangedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "LastChangedBy_Id" });
            DropColumn("dbo.Issues", "LastChangedBy_Id");
            RenameIndex(table: "dbo.Issues", name: "IX_AssignedTo_Id", newName: "IX_Agent_Id");
            RenameColumn(table: "dbo.Issues", name: "AssignedTo_Id", newName: "Agent_Id");
        }
    }
}
