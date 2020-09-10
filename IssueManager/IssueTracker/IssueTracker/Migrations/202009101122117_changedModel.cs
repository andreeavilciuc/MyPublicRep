namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Issues", name: "Agent_Id", newName: "LastModifiedBy_Id");
            RenameIndex(table: "dbo.Issues", name: "IX_Agent_Id", newName: "IX_LastModifiedBy_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Issues", name: "IX_LastModifiedBy_Id", newName: "IX_Agent_Id");
            RenameColumn(table: "dbo.Issues", name: "LastModifiedBy_Id", newName: "Agent_Id");
        }
    }
}
