namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IssueModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubmitDate = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        SubmitterID = c.Int(nullable: false),
                        Submitter_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Submitter_Id)
                .Index(t => t.Submitter_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "Submitter_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "Submitter_Id" });
            DropTable("dbo.Issues");
        }
    }
}
