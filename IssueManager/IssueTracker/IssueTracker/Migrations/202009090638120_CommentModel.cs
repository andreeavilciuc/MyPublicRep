namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IssueID = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Issues", t => t.IssueID, cascadeDelete: true)
                .Index(t => t.IssueID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "IssueID", "dbo.Issues");
            DropIndex("dbo.Comments", new[] { "IssueID" });
            DropTable("dbo.Comments");
        }
    }
}
