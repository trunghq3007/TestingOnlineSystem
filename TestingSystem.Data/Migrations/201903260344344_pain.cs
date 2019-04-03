namespace TestingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tests", "Candidates_CandidateID", "dbo.Candidates");
            DropForeignKey("dbo.ExamPaperExams", "ExamID", "dbo.Exams");
            DropForeignKey("dbo.Tests", "Exam_ExamID", "dbo.Exams");
            DropForeignKey("dbo.ExamPaperExams", "ExamPaperID", "dbo.ExamPapers");
            DropIndex("dbo.Tests", new[] { "Candidates_CandidateID" });
            DropIndex("dbo.Tests", new[] { "Exam_ExamID" });
            DropIndex("dbo.ExamPaperExams", new[] { "ExamID" });
            DropIndex("dbo.ExamPaperExams", new[] { "ExamPaperID" });
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        TestResultID = c.Int(nullable: false, identity: true),
                        TestID = c.Int(nullable: false),
                        CandidateID = c.Int(nullable: false),
                        TestName = c.String(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Score = c.Int(nullable: false),
                        QuestionID = c.Int(nullable: false),
                        AnswerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestResultID)
                .ForeignKey("dbo.Candidates", t => t.CandidateID, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.TestID, cascadeDelete: true)
                .Index(t => t.TestID)
                .Index(t => t.CandidateID);
            
            CreateTable(
                "dbo.ExamTests",
                c => new
                    {
                        ExamTestID = c.Int(nullable: false, identity: true),
                        ExamID = c.Int(nullable: false),
                        TestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExamTestID)
                .ForeignKey("dbo.Exams", t => t.ExamID, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.TestID, cascadeDelete: true)
                .Index(t => t.ExamID)
                .Index(t => t.TestID);
            
            AddColumn("dbo.Tests", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tests", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tests", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tests", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Tests", "ModifiedBy", c => c.Int());
            AddColumn("dbo.Tests", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.Exams", "ExamCode", c => c.String());
            DropColumn("dbo.Tests", "Candidates_CandidateID");
            DropColumn("dbo.Tests", "Exam_ExamID");
            DropColumn("dbo.ExamPapers", "ExamPaperCode");
            DropTable("dbo.ExamPaperExams");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExamPaperExams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExamID = c.Int(nullable: false),
                        ExamPaperID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ExamPapers", "ExamPaperCode", c => c.String());
            AddColumn("dbo.Tests", "Exam_ExamID", c => c.Int());
            AddColumn("dbo.Tests", "Candidates_CandidateID", c => c.Int());
            DropForeignKey("dbo.TestResults", "TestID", "dbo.Tests");
            DropForeignKey("dbo.ExamTests", "TestID", "dbo.Tests");
            DropForeignKey("dbo.ExamTests", "ExamID", "dbo.Exams");
            DropForeignKey("dbo.TestResults", "CandidateID", "dbo.Candidates");
            DropIndex("dbo.ExamTests", new[] { "TestID" });
            DropIndex("dbo.ExamTests", new[] { "ExamID" });
            DropIndex("dbo.TestResults", new[] { "CandidateID" });
            DropIndex("dbo.TestResults", new[] { "TestID" });
            DropColumn("dbo.Exams", "ExamCode");
            DropColumn("dbo.Tests", "ModifiedDate");
            DropColumn("dbo.Tests", "ModifiedBy");
            DropColumn("dbo.Tests", "CreatedBy");
            DropColumn("dbo.Tests", "EndDate");
            DropColumn("dbo.Tests", "StartDate");
            DropColumn("dbo.Tests", "IsActive");
            DropTable("dbo.ExamTests");
            DropTable("dbo.TestResults");
            CreateIndex("dbo.ExamPaperExams", "ExamPaperID");
            CreateIndex("dbo.ExamPaperExams", "ExamID");
            CreateIndex("dbo.Tests", "Exam_ExamID");
            CreateIndex("dbo.Tests", "Candidates_CandidateID");
            AddForeignKey("dbo.ExamPaperExams", "ExamPaperID", "dbo.ExamPapers", "ExamPaperID", cascadeDelete: true);
            AddForeignKey("dbo.Tests", "Exam_ExamID", "dbo.Exams", "ExamID");
            AddForeignKey("dbo.ExamPaperExams", "ExamID", "dbo.Exams", "ExamID", cascadeDelete: true);
            AddForeignKey("dbo.Tests", "Candidates_CandidateID", "dbo.Candidates", "CandidateID");
        }
    }
}
