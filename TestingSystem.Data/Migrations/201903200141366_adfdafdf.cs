namespace TestingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adfdafdf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestResults", "CandidatesID", "dbo.Candidates");
            DropForeignKey("dbo.TestResults", "TestID", "dbo.Tests");
            DropForeignKey("dbo.Candidates", "UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.TestResults", new[] { "CandidatesID" });
            DropIndex("dbo.TestResults", new[] { "TestID" });
            RenameColumn(table: "dbo.Candidates", name: "UserId", newName: "CandidateID");
            RenameIndex(table: "dbo.Candidates", name: "IX_UserId", newName: "IX_CandidateID");
            DropPrimaryKey("dbo.Candidates");
            AddColumn("dbo.Tests", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.Tests", "Candidates_CandidateID", c => c.Int());
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.Candidates", "CandidateID");
            CreateIndex("dbo.Users", "UserName", unique: true);
            CreateIndex("dbo.Tests", "Candidates_CandidateID");
            AddForeignKey("dbo.Tests", "Candidates_CandidateID", "dbo.Candidates", "CandidateID");
            AddForeignKey("dbo.Candidates", "CandidateID", "dbo.Users", "UserId");
            DropColumn("dbo.Candidates", "CandidatesID");
            DropTable("dbo.TestResults");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        TestResultID = c.Int(nullable: false, identity: true),
                        CandidatesID = c.Int(nullable: false),
                        TestID = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TestResultID);
            
            AddColumn("dbo.Candidates", "CandidatesID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Candidates", "CandidateID", "dbo.Users");
            DropForeignKey("dbo.Tests", "Candidates_CandidateID", "dbo.Candidates");
            DropIndex("dbo.Tests", new[] { "Candidates_CandidateID" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropPrimaryKey("dbo.Candidates");
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Tests", "Candidates_CandidateID");
            DropColumn("dbo.Tests", "Score");
            AddPrimaryKey("dbo.Candidates", "CandidatesID");
            RenameIndex(table: "dbo.Candidates", name: "IX_CandidateID", newName: "IX_UserId");
            RenameColumn(table: "dbo.Candidates", name: "CandidateID", newName: "UserId");
            CreateIndex("dbo.TestResults", "TestID");
            CreateIndex("dbo.TestResults", "CandidatesID");
            CreateIndex("dbo.Users", "UserName", unique: true);
            AddForeignKey("dbo.Candidates", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.TestResults", "TestID", "dbo.Tests", "TestID", cascadeDelete: true);
            AddForeignKey("dbo.TestResults", "CandidatesID", "dbo.Candidates", "CandidatesID", cascadeDelete: true);
        }
    }
}
