namespace TestingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createnewtablecandidatestest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandidatesTests",
                c => new
                    {
                        CandidatesTestID = c.Int(nullable: false, identity: true),
                        TestID = c.Int(nullable: false),
                        CandidateID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CandidatesTestID)
                .ForeignKey("dbo.Candidates", t => t.CandidateID, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.TestID, cascadeDelete: true)
                .Index(t => t.TestID)
                .Index(t => t.CandidateID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidatesTests", "TestID", "dbo.Tests");
            DropForeignKey("dbo.CandidatesTests", "CandidateID", "dbo.Candidates");
            DropIndex("dbo.CandidatesTests", new[] { "CandidateID" });
            DropIndex("dbo.CandidatesTests", new[] { "TestID" });
            DropTable("dbo.CandidatesTests");
        }
    }
}
