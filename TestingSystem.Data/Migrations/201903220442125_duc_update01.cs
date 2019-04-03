namespace TestingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duc_update01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tests", "ExamID", "dbo.Exams");
            DropIndex("dbo.Tests", new[] { "ExamID" });
            RenameColumn(table: "dbo.Tests", name: "ExamID", newName: "Exam_ExamID");
            CreateTable(
                "dbo.ExamPaperExams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExamID = c.Int(nullable: false),
                        ExamPaperID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.ExamID, cascadeDelete: true)
                .ForeignKey("dbo.ExamPapers", t => t.ExamPaperID, cascadeDelete: true)
                .Index(t => t.ExamID)
                .Index(t => t.ExamPaperID);
            
            AlterColumn("dbo.Tests", "Exam_ExamID", c => c.Int());
            CreateIndex("dbo.Tests", "Exam_ExamID");
            AddForeignKey("dbo.Tests", "Exam_ExamID", "dbo.Exams", "ExamID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "Exam_ExamID", "dbo.Exams");
            DropForeignKey("dbo.ExamPaperExams", "ExamPaperID", "dbo.ExamPapers");
            DropForeignKey("dbo.ExamPaperExams", "ExamID", "dbo.Exams");
            DropIndex("dbo.ExamPaperExams", new[] { "ExamPaperID" });
            DropIndex("dbo.ExamPaperExams", new[] { "ExamID" });
            DropIndex("dbo.Tests", new[] { "Exam_ExamID" });
            AlterColumn("dbo.Tests", "Exam_ExamID", c => c.Int(nullable: false));
            DropTable("dbo.ExamPaperExams");
            RenameColumn(table: "dbo.Tests", name: "Exam_ExamID", newName: "ExamID");
            CreateIndex("dbo.Tests", "ExamID");
            AddForeignKey("dbo.Tests", "ExamID", "dbo.Exams", "ExamID", cascadeDelete: true);
        }
    }
}
